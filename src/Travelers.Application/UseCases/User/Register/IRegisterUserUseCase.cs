using Travelers.Communication.Requests;
using Travelers.Communication.Responses;

namespace Travelers.Application.UseCases.User;

public interface IRegisterUserUseCase
{
    Task<UserResponse> ExecuteAsync(UserRequest request, CancellationToken ct);
}