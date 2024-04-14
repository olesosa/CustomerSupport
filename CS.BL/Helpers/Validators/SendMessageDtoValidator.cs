using CS.DOM.DTO;
using FluentValidation;

namespace CS.BL.Helpers.Validators;

public class SendMessageDtoValidator : AbstractValidator<ChatMessageDto> // todo rewrite this
{
    public SendMessageDtoValidator()
    {
        RuleFor(s=>s.DialogId)            
            .NotNull()
            .WithMessage("Dialog Id is required");
        
        RuleFor(s=>s.Text)            
            .NotNull()
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Invalid Message");

        RuleFor(s => s.WhenSend)
            .NotEmpty()
            .NotNull()
            .WithMessage("Invalid date time");
    }
}