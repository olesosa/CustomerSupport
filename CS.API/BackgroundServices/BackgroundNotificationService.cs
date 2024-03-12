using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CS.Api.BackgroundServices;

public class BackgroundNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public BackgroundNotificationService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //await CheckUnReadMessages();
            //await CheckNewTickets();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task CheckUnReadMessages()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var messages = await context.Messages
            .Include(m => m.User)
            .Where(m => !m.IsRead && m.User.RoleName == "User")
            .Where(t => t.WhenSend.AddMinutes(5) < DateTime.Now)
            .ToListAsync();

        var tasks = messages.Select(emailService.SendEmail);
        await Task.WhenAll(tasks);
    }

    public async Task CheckNewTickets()
    {
        using var scope = _scopeFactory.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var tickets = await context.Tickets
            .Include(t => t.Details)
            .Where(t => t.IsAssigned && t.Details.CreationTime.AddMinutes(1) < DateTime.Now)
            .ToListAsync();

        var tasks = tickets.Select(emailService.SendEmail);
        await Task.WhenAll(tasks);
    }
}