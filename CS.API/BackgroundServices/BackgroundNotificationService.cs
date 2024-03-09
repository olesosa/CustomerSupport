using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CS.Api.BackgroundServices;

public class BackgroundDialogService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    //private readonly IEmailService _emailService;

    public BackgroundDialogService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckIgnoredMessages();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task CheckIgnoredMessages()
    {
        using var scope = _scopeFactory.CreateScope();
                
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var messages = await context.Messages
            .Include(m => m.User)
            .Where(m => !m.IsRead && m.User.RoleName == "User")
            .Where(t => t.WhenSend.AddMinutes(5) < DateTime.Now)
            .ToListAsync();

        // TODO send email
    }
}