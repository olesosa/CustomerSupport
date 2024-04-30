using CS.BL.Helpers;
using CS.BL.Hubs;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DOM.DTO;
using CS.DOM.Enums;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

public class SignalrService : ISignalrService
{
    private readonly IHubContext<DialogHub> _hubContext;
    private readonly IMessageService _messageService;
    private readonly ApplicationContext _context;
    private readonly IAttachmentService _attachmentService;

    public SignalrService(IHubContext<DialogHub> hubContext, IMessageService messageService,
        ApplicationContext context, IAttachmentService attachmentService)
    {
        _hubContext = hubContext;
        _messageService = messageService;
        _context = context;
        _attachmentService = attachmentService;
    }

    public async Task<MessageDto> SendMessage(PostMessage message, Guid dialogId,UserInfoDto user)
    {
        var dialog = await _context.Dialogs
            .Include(d => d.Ticket)
            .FirstOrDefaultAsync(d => d.Id == dialogId);

        if (dialog == null)
        {
            throw new ApiException(404, "Dialog not found");
        }

        var receiverId = user.RoleName == UserRoles.User 
            ? dialog.Ticket.AdminId 
            : dialog.Ticket.CustomerId;
        
        var sentMessage = await _messageService.SaveMessage(message.Text, dialogId, user.Id);

        var attachments = new List<Guid>();

        if (message.Files.Files.Count != 0)
        {
            foreach (var file in message.Files.Files)
            {
                attachments.Add(await _attachmentService.AddMessageAttachment(file, sentMessage.Id));
            }
        }
        
        var email = await _context.Users
            .Where(u => u.Id == receiverId)
            .Select(u => u.Email)
            .FirstOrDefaultAsync();

        await _hubContext.Clients
            .User(email!)
            .SendAsync("ReceiveMessage",
                message.Text,
                user.UserName,
                dialog.Id,
                user.Id,
                attachments);

        return new MessageDto()
        {
            DialogId = dialogId,
            UserName = user.UserName,
            WhenSended = DateTime.Now,
            Attachments = attachments,
            UserId = user.Id,
            Text = message.Text
        };
    }
}