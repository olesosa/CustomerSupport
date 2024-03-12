using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class TicketSolveDtoValidator : AbstractValidator<TicketSolveDto>
{
    public TicketSolveDtoValidator()
    {
        RuleFor(t => t.TicketId)
            .NotNull()
            .WithMessage("Ticket Id is required");
        
        RuleFor(t => t.IsSolved)
            .NotNull()
            .WithMessage("Invalid Dto Model");
    }
}