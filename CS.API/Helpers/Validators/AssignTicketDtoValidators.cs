using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class AssignTicketDtoValidators  : AbstractValidator<AssignTicketDto>
{
    public AssignTicketDtoValidators()
    {
        RuleFor(a => a.adminId)
            .NotNull()
            .WithMessage("Admin Id id required");
        
        RuleFor(a => a.ticketId)
            .NotNull()
            .WithMessage("Ticket Id is required");

    }
}