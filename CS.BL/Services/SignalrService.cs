using CS.BL.Hubs;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

public class SignalrService : ISignalrService
{
    private readonly IHubContext<DialogHub> _hubContext;
    private readonly IMessageService _messageService;
    private readonly ApplicationContext _context;

    public SignalrService(IHubContext<DialogHub> hubContext, IMessageService messageService, ApplicationContext context)
    {
        _hubContext = hubContext;
        _messageService = messageService;
        _context = context;
    }

    public async Task SendMessage(ChatMessageDto message, UserInfoDto user)
    {
        var dialog = await _context.Dialogs
            .Include(d => d.Ticket)
            .FirstOrDefaultAsync(d => d.Id == message.DialogId);

        if (dialog == null)
        {
            throw new ApiException(404, "Dialog not found");
        }

        var receiverId = user.RoleName == "User" ? dialog.Ticket.AdminId : dialog.Ticket.CustomerId;

        var email = _context.Users.Where(u => u.Id == receiverId).Select(u => u.Email).ToString();
        
        await _hubContext.Clients
            .User(user.Email) // todo temp change to email
            .SendAsync("ReceiveMessage", message.Text, user.UserName);
        
        await _messageService.SaveMessage(message, user.Id);

    }
}
