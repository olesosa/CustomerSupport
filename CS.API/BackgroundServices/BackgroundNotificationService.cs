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
            await CheckUnReadMessages();
            await CheckNewTickets();

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

        foreach (var message in messages)
        {
            message.IsRead = true;
        }
        
        var tasks = messages.Select(emailService.SendEmail);
        await Task.WhenAll(tasks);
    }

    private async Task CheckNewTickets()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var tickets = await context.Tickets
            .Include(t => t.Details)
            .Where(t =>
                t.Details.IsAssigned &&
                t.Details.CreationTime.AddMinutes(1) < DateTime.Now &&
                t.Details.HasReceived.HasValue &&
                t.Details.HasReceived == false)
            .ToListAsync();

        foreach (var ticket in tickets)
        {
            ticket.Details.HasReceived = true;
        }
        
        var tasks = tickets.Select(emailService.SendEmail);
        await Task.WhenAll(tasks);
    }
}