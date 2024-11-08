using Travelers.Domain.Entities;

namespace Travelers.Domain.Interfaces.Security.Token;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}