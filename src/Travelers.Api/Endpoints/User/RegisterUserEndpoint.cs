using FastEndpoints;
using Travelers.Application.UseCases.User;
using Travelers.Communication.Requests;
using Travelers.Communication.Responses;

namespace Travelers.Api.Endpoints.User;

public class RegisterUserEndpoint : Endpoint<UserRequest, UserResponse>
{
    private readonly IRegisterUserUseCase _registerUserUseCase;
    public RegisterUserEndpoint(IRegisterUserUseCase registerUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
    }
    
    public override void Configure()
    {
        Post("");
        Group<UserGroup>();
        Summary(s => s.Summary = "Register a new user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserRequest req, CancellationToken ct)
    {
        var response = await _registerUserUseCase.ExecuteAsync(req, ct);
        
        await SendOkAsync(response, ct);
    }
}