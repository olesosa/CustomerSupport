using CS.DOM.DTO;
using FluentValidation;

namespace CS.BL.Helpers.Validators;

public class ChatMessageDtoValidator : AbstractValidator<ChatMessageDto>
{
    public ChatMessageDtoValidator()
    {
        RuleFor(s=>s.DialogId)            
            .NotNull()
            .WithMessage("Dialog Id is required");
        
        RuleFor(s=>s.Text)            
            .NotNull()
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Invalid Message");
    }
}