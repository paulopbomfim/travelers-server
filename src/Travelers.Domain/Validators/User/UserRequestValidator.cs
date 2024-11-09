using FluentValidation;
using Travelers.Communication.Requests;
using Travelers.Exception;

namespace Travelers.Domain.Validators.User;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(user => user.TxName)
            .NotEmpty().WithMessage(ErrorMessagesResource.NAME_EMPTY);
        
        RuleFor(user => user.TxEmail)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.TxEmail), ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessagesResource.EMAIL_INVALID);
        
        RuleFor(user => user.TxPassword).SetValidator(new PasswordValidator<UserRequest>());
    }
}