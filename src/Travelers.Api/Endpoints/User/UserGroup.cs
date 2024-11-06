using FastEndpoints;

namespace Travelers.Api.Endpoints.User;

public sealed class UserGroup : Group
{
    public UserGroup()
    {
        Configure("user", _ => {});
    }
}