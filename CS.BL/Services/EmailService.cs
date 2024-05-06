using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;


namespace CS.BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private string _frontEndUrl = Environments.FrontAddress;

        public EmailService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> SendEmail(Message message)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == message.UserId);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("alexbobr1337@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Unread message";

            email.Body = email.Body = new TextPart(TextFormat.Html)
            {
                //better extract to separate html file
                Text = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Your Support Service</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }}
        button {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }}
        button:hover {{
            background-color: #0056b3;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Hello {user.UserName},</h1>
        <p>You have unread message(s) in your inbox.</p>
        <p>Message Text: {message.MessageText}</p>
        <p>From user: {user.UserName} ({user.Email})</p>
        <p><button onclick=""window.location.href='{{$""{_frontEndUrl}dialogs""}}'"">Dialog</button></p>
    </div>
</body>
</html>
"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("alexbobr1337@gmail.com", "hklu emus pdgn dpxk");//not safe to store creds here

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            return true;
        }

        public async Task<bool> SendEmail(Ticket ticket)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            
            var admin = await context.Users.FirstOrDefaultAsync(u => u.Id == ticket.AdminId);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == ticket.CustomerId);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("alexbobr1337@gmail.com"));
            email.To.Add(MailboxAddress.Parse(admin.Email));
            email.Subject = "New Ticket";

            email.Body = email.Body = new TextPart(TextFormat.Html)
            {
                //better extract to separate html file
                Text = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>New Ticket Assignment</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }}
        a.button {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }}
        a.button:hover {{
            background-color: #0056b3;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Hello {admin.UserName},</h1>
        <p>You have a new ticket assigned to you.</p>
        <p>Short info:</p>
        <p>Number: {ticket.Number}</p>
        <p>Topic: {ticket.Topic}</p>
        <p>Customer: {user.Email}</p>
        <p><a href=""{_frontEndUrl}tickets/{ticket.Number}"" class=""button"">View Ticket</a></p>
    </div>
</body>
</html>
"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("alexbobr1337@gmail.com", "hklu emus pdgn dpxk");

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            return true;
        }
    }
}