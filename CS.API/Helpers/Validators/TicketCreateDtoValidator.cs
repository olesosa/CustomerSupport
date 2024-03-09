using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
{
    public TicketCreateDtoValidator()
    {
        RuleFor(t => t.Id)
            .NotNull()
            .WithMessage("Id is required");
        
        RuleFor(t => t.CustomerId)
            .NotNull()
            .WithMessage("Customer Id is required");

        RuleFor(t => t.RequestType)
            .NotNull()
            .Length(5)
            .WithMessage("Invalid Request Type");

        RuleFor(t => t.Topic)
            .NotNull()
            .Length(5, 255)
            .WithMessage("Invalid Topic");

        RuleFor(t => t.Description)
            .NotNull()
            .MinimumLength(5)
            .WithMessage("Invalid Description");

        RuleFor(t => t.CreationTime)
            .NotNull()
            .WithMessage("Creation time is required");
    }
}