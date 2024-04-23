using CS.DOM.DTO;
using FluentValidation;

namespace CS.BL.Helpers.Validators;

public class TicketUpdateDtoValidator : AbstractValidator<TicketUpdateDto>
{
    public TicketUpdateDtoValidator()
    {
        RuleFor(t => t.IsSolved)
            .NotNull()
            .WithMessage("IsSolved property is required");
        
        RuleFor(t => t.IsClosed)
            .NotNull()
            .WithMessage("IsClosed property is required");
    }
}