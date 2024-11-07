using Travelers.Communication.Requests;
using Travelers.Communication.Responses;

namespace Travelers.Domain.Mappings.User;

public static class UserMappingProfile
{
    public static Entities.User MapToEntity(this UserRequest request)
    {
        return new Entities.User
        {
            TxName = request.TxName,
            TxEmail = request.TxEmail,
            TxPassword = request.TxPassword
        };
    }

    public static UserResponse MapToResponse(this Entities.User user, string accessToken)
    {
        return new UserResponse
        {
            TxName = user.TxName,
            AccessToken = accessToken
        };
    }
}