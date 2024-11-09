using FluentAssertions;
using FluentValidation;
using Travelers.Communication.Requests;
using Travelers.Domain.Validators;

namespace Validator.Test.User;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Failed_Validation_Password_Invalid(string password)
    {
        //arrange
        var validator = new PasswordValidator<UserRequest>();
        
        //act
        var result = validator.IsValid(new ValidationContext<UserRequest>(new UserRequest()), password);
        
        //assert
        result.Should().BeFalse();
    }
}