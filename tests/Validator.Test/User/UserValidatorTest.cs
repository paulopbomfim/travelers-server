using CommonTestUtilities.Requests.User;
using FluentAssertions;
using Travelers.Domain.Validators.User;
using Travelers.Exception;

namespace Validator.Test.User;

public class UserValidatorTest
{
    [Fact]
    public void Successful_Validation()
    {
        //arrange
        var validator = new UserRequestValidator();
        var userRequest = UserRequestBuilder.Build();

        //act
        var result = validator.Validate(userRequest);

        //assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Failed_Validation_Name_Empty(string name)
    {
        //arrange
        var validator = new UserRequestValidator();
        var userRequest = UserRequestBuilder.Build();
        userRequest.TxName = name;

        //act
        var result = validator.Validate(userRequest);
        
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.NAME_EMPTY));
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Failed_Validation_Email_Empty(string email)
    {
        //arrange
        var validator = new UserRequestValidator();
        var userRequest = UserRequestBuilder.Build();
        userRequest.TxEmail = email;
        
        //act
        var result = validator.Validate(userRequest);
        
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.EMAIL_EMPTY));
    }

    [Fact]
    public void Failed_Validation_Email_Invalid()
    {
        //arrange
        var validator = new UserRequestValidator();
        var userRequest = UserRequestBuilder.Build();
        userRequest.TxEmail = "johndoe.com";
        
        //act
        var result = validator.Validate(userRequest);
        
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.EMAIL_INVALID));
    }

    [Fact]
    public void Failed_Validation_Password_Empty()
    {
        //arrange
        var validator = new UserRequestValidator();
        var userRequest = UserRequestBuilder.Build();
        userRequest.TxPassword = string.Empty;
        
        //act
        var result = validator.Validate(userRequest);
        
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.INVALID_PASSWORD));
    }
}