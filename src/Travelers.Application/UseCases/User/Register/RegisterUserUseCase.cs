using FluentValidation.Results;
using Travelers.Communication.Requests;
using Travelers.Communication.Responses;
using Travelers.Domain.Interfaces.Repositories;
using Travelers.Domain.Interfaces.Security.Cryptography;
using Travelers.Domain.Interfaces.Security.Token;
using Travelers.Domain.Mappings.User;
using Travelers.Domain.Repositories;
using Travelers.Domain.Validators.User;
using Travelers.Exception;
using Travelers.Exception.ExceptionBase;

namespace Travelers.Application.UseCases.User;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _uow;

    public RegisterUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork uow, IPasswordEncryptor passwordEncryptor,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _passwordEncryptor = passwordEncryptor;
        _accessTokenGenerator = accessTokenGenerator;
        _uow = uow;
    }

    public async Task<UserResponse> ExecuteAsync(UserRequest request, CancellationToken ct = default)
    {
        await Validate(request, ct);

        var user = request.MapToEntity();

        user.TxPassword = _passwordEncryptor.Encrypt(request.TxPassword);
        user.CoUserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user, ct);
        await _uow.Commit(ct);

        var token = _accessTokenGenerator.Generate(user);
        return user.MapToResponse(token);
    }

    private async Task Validate(UserRequest request, CancellationToken ct)
    {
        var validation = await new UserRequestValidator().ValidateAsync(request, ct);
        
        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.TxEmail, ct);

        if (emailExists)
            validation.Errors.Add(new ValidationFailure(string.Empty, ErrorMessagesResource.EMAIL_ALREADY_REGISTERED));
        
        if (validation.IsValid) return;
        
        var errorMessages = validation.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}