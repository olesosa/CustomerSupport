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
                email.Subject = "Email verification";
                email.Body = email.Body = new TextPart(TextFormat.Html)
                {
                    // TODO write html email
                };
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

        public Task<bool> SendEmail(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}