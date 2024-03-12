using CS.DOM.DTO;
using FluentValidation;

namespace CS.API.Helpers.Validators;

public class SendMessageDtoValidator : AbstractValidator<SendMessageDto>
{
    public SendMessageDtoValidator()
    {
        RuleFor(s => s.Id)
            .NotNull()
            .WithMessage("Id is required");
        
        RuleFor(s=>s.DialogId)            
            .NotNull()
            .WithMessage("Dialog Id is required");
        
        RuleFor(s=>s.MessageText)            
            .NotNull()
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Invalid Message");
    }
}