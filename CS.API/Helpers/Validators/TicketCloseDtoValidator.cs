using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class TicketCloseDtoValidator : AbstractValidator<TicketCloseDto>
{
    public TicketCloseDtoValidator()
    {
        RuleFor(t => t.TicketId)
            .NotNull()
            .WithMessage("Ticket Id is required");
        
        RuleFor(t => t.IsClosed)
            .NotNull()
            .WithMessage("Invalid Dto Model");
    }
}