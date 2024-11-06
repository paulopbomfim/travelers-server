using FastEndpoints;
using Travelers.Communication.Requests;
using Travelers.Communication.Responses;

namespace Travelers.Api.Endpoints.User;

public class RegisterUserEndpoint : Endpoint<UserRequest, UserResponse>
{
    public override void Configure()
    {
        Post("");
        Group<UserGroup>();
        Summary(s => s.Summary = "Register a new user");
    }

    public override async Task HandleAsync(UserRequest req, CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}