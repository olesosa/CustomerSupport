using System.Net.Mail;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CS.BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationContext _context;
        private readonly IUserService _userService;
        private string _frontEndUrl = Environments.FrontAddress;

        public EmailService(IUserService userService, ApplicationContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<bool> SendEmail(Message message)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == message.UserId);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("alexbobr1337@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Unread message";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #f4f4f4;
            }}
            .container {{
                max-width: 600px;
                margin: auto;
                padding: 20px;
                background-color: #fff;
                border-radius: 5px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }}
            h1 {{
                color: #333;
            }}
            p {{
                color: #666;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Hello {user.UserName},</h1>
            <p>You have unread message(s) in your inbox.</p>
            <p>Message Text: {message.MessageText}</p>
            <p>From user: {user.UserName} {user.Email}</p>
        </div>
    </body>
    </html>
";

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("alexbobr1337@gmail.com", "hklu emus pdgn dpxk");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendEmail(Ticket ticket)
        {
            try
            {
                var admin = await _context.Users.FirstOrDefaultAsync(u => u.Id == ticket.AdminId);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ticket.CustomerId);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("alexbobr1337@gmail.com"));
                email.To.Add(MailboxAddress.Parse(admin.Email));
                email.Subject = "Unread message";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #f4f4f4;
            }}
            .container {{
                max-width: 600px;
                margin: auto;
                padding: 20px;
                background-color: #fff;
                border-radius: 5px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }}
            h1 {{
                color: #333;
            }}
            p {{
                color: #666;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Hello {admin.UserName},</h1>
            <p>You have new ticked assigned for you</p>
            <p>Short info:</p>
            <p>Number: {ticket.Number}</p>
            <p>Topic: {ticket.Topic}</p>
            <p>Customer: {user.Email}</p>
            <p><a href=""{$"{_frontEndUrl}tickets/{ticket.Id}"}"">Link the the ticket</a></p>
        </div>
    </body>
    </html>
";

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("alexbobr1337@gmail.com", "hklu emus pdgn dpxk");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}