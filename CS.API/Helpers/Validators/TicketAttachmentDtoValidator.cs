using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class TicketAttachmentDtoValidator : AbstractValidator<TicketAttachmentDto>
{
    public TicketAttachmentDtoValidator()
    {
        RuleFor(t => t.Id)
            .NotNull()
            .WithMessage("Id is required");
        
        RuleFor(t => t.TicketId)
            .NotNull()
            .WithMessage("Ticket Id is required");

        RuleFor(t => t.FilePath)
            .NotNull()
            .MaximumLength(50)
            .WithMessage("Invalid FilePath");
    }
}