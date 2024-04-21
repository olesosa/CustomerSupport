using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CS.BL.Hubs;

[Authorize]
public class DialogHub : Hub
{
    
}