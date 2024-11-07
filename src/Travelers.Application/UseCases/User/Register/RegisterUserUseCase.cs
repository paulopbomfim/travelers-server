using Travelers.Communication.Requests;
using Travelers.Communication.Responses;
using Travelers.Domain.Interfaces.Repositories;
using Travelers.Domain.Interfaces.Security.Cryptography;
using Travelers.Domain.Mappings.User;
using Travelers.Domain.Repositories;

namespace Travelers.Application.UseCases.User;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IUnitOfWork _uow;

    public RegisterUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork uow, IPasswordEncryptor passwordEncryptor)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _passwordEncryptor = passwordEncryptor;
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

        var token = "";
        return user.MapToResponse(token);
    }

    private async Task Validate(UserRequest request, CancellationToken ct)
    {
        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.TxEmail, ct);
        
        if (emailExists)
            throw new Exception("Email already registered.");
    }
}