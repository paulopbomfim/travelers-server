using Bogus;
using Travelers.Communication.Requests;

namespace CommonTestUtilities.Requests.User;

public class UserRequestBuilder
{
    public static UserRequest Build()
    {
        return new Faker<UserRequest>()
            .RuleFor(u => u.TxName, f => f.Name.FullName())
            .RuleFor(u => u.TxEmail, f => f.Internet.Email())
            .RuleFor(u => u.TxPassword, f => f.Internet.Password(prefix: "!Aa1"));
    }
}