using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CS.BL.Hubs;

public class CustomEmailProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirstValue(ClaimTypes.Email);
    }
}