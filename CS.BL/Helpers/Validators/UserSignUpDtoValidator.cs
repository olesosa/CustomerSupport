using CS.DOM.DTO;
using FluentValidation;

namespace CS.BL.Helpers.Validators;

public class UserSignUpDtoValidator: AbstractValidator<UserSignUpDto>
{
    public UserSignUpDtoValidator()
    {
        RuleFor(u => u.Id)
            .NotNull()
            .WithMessage("Id is required");

        RuleFor(u => u.Email)
            .NotNull()
            .EmailAddress()
            .WithMessage("Email is required");
        
        RuleFor(u => u.RoleName)
            .NotNull()
            .WithMessage("Role is required");
    }
}