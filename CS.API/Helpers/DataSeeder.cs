using CS.DAL.DataAccess;
using CS.DAL.Models;

namespace CS.API.Helpers
{
    public class DataSeeder
    {
        readonly ApplicationContext _context;

        public DataSeeder(ApplicationContext context)
        {
            _context = context;
        }

        public void SeedAllData()
        {
            SeedUsers();
            SeedTicketDetails();
            SeedTickets();
            SeedTicketAttachments();
            SeedDialogs();
            SeedMessages();
            SeedMessageAttachments();
            SeedDialogsWithMessages();
        }

        public void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>()
                {
                    new User() { Email = "userEmail1@email.com", RoleName = "User", },
                    new User() { Email = "userEmail2@email.com", RoleName = "User" },
                    new User() { Email = "userEmail3@email.com", RoleName = "User" },

                    new User() { Email = "adminEmail1@email.com", RoleName = "Admin" },
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }

        public void SeedTicketDetails()
        {
            if (!_context.TicketDetails.Any())
            {
                // var details = new List<TicketDetails>()
                // {
                //     new TicketDetails()
                //         { Topic = "Ticket Topic 1", Description = "Ticket Description 1", CreationTime = DateTime.Now },
                //     new TicketDetails()
                //         { Topic = "Ticket Topic 2", Description = "Ticket Description 2", CreationTime = DateTime.Now },
                //     new TicketDetails()
                //         { Topic = "Ticket Topic 3", Description = "Ticket Description 3", CreationTime = DateTime.Now },
                // };

                //_context.TicketDetails.AddRange(details);
                _context.SaveChanges();
            }
        }

        public void SeedTickets()
        {
            if (!_context.Tickets.Any())
            {
                var tickets = new List<Ticket>()
                {
                    // new Ticket()
                    // {
                    //     CustomerId = _context.Users.FirstOrDefault(u => u.Email == "userEmail1@email.com").Id,
                    //     AdminId = _context.Users.FirstOrDefault(a => a.Email == "adminEmail1@email.com").Id,
                    //     DetailsId = _context.TicketDetails.FirstOrDefault(d => d.Topic == "Ticket Topic 1").Id,
                    //     RequestType = "Request 1",
                    //     IsAssigned = true,
                    //     IsClosed = false,
                    //     IsSolved = false,
                    // },

                    // new Ticket()
                    // {
                    //     CustomerId = _context.Users.FirstOrDefault(u => u.Email == "userEmail2@email.com").Id,
                    //     DetailsId = _context.TicketDetails.FirstOrDefault(d => d.Topic == "Ticket Topic 2").Id,
                    //     RequestType = "Request 2",
                    //     IsAssigned = false,
                    //     IsClosed = false,
                    //     IsSolved = false,
                    // }
                };

                _context.Tickets.AddRange(tickets);
                _context.SaveChanges();
            }
        }

        public void SeedTicketAttachments()
        {
            if (!_context.TicketAttachments.Any())
            {
                var attachments = new List<TicketAttachment>()
                {
                    new TicketAttachment()
                    {
                        TicketId = _context.Tickets.FirstOrDefault(t => t.RequestType == "Request 1").Id,
                        FilePath = "FilePath 1"
                    },
                    new TicketAttachment()
                    {
                        TicketId = _context.Tickets.FirstOrDefault(t => t.RequestType == "Request 1").Id,
                        FilePath = "FilePath 2"
                    },
                    new TicketAttachment()
                    {
                        TicketId = _context.Tickets.FirstOrDefault(t => t.RequestType == "Request 2").Id,
                        FilePath = "FilePath 3"
                    },
                };

                _context.TicketAttachments.AddRange(attachments);
                _context.SaveChanges();
            }
        }

        public void SeedDialogs()
        {
            if (!_context.Dialogs.Any())
            {
                var dialogs = new List<Dialog>()
                {
                    new Dialog()
                    {
                        TicketId = _context.Tickets.FirstOrDefault(t => t.RequestType == "Request 1").Id,
                    },
                };

                _context.Dialogs.AddRange(dialogs);
                _context.SaveChanges();
            }
        }

        public void SeedMessages()
        {
            if (!_context.Messages.Any())
            {
                var messages = new List<Message>()
                {
                    new Message()
                    {
                        WhenSend = DateTime.Now, MessageText = "Text 1", IsRead = true,
                        UserId = _context.Users.FirstOrDefault(u => u.Email == "userEmail1@email.com").Id,
                        DialogId = _context.Dialogs.FirstOrDefault(d => d.TicketId ==
                                                                        _context.Tickets.FirstOrDefault(t =>
                                                                            t.RequestType == "Request 1").Id).Id,
                    },

                    new Message()
                    {
                        WhenSend = DateTime.Now, MessageText = "Text 2", IsRead = true,
                        UserId = _context.Users.FirstOrDefault(u => u.Email == "adminEmail1@email.com").Id,
                        DialogId = _context.Dialogs.FirstOrDefault(d => d.TicketId ==
                                                                        _context.Tickets.FirstOrDefault(t =>
                                                                            t.RequestType == "Request 1").Id).Id,
                    }
                };
            }
        }

        public void SeedMessageAttachments()
        {
            if (!_context.MessageAttachments.Any())
            {
                var attachments = new List<MessageAttachment>()
                {
                    new MessageAttachment()
                    {
                        MessageId = _context.Messages.FirstOrDefault(m => m.MessageText == "Text 1").Id,
                        FilePath = "FilePath 1"
                    },
                    new MessageAttachment()
                    {
                        MessageId = _context.Messages.FirstOrDefault(m => m.MessageText == "Text 1").Id,
                        FilePath = "FilePath 2"
                    },
                };

                _context.MessageAttachments.AddRange(attachments);
                _context.SaveChanges();
            }
        }

        public void SeedDialogsWithMessages()
        {
            if (_context.Dialogs.FirstOrDefault(d=> d.Ticket.RequestType == "Request 1") != null)
            {
                var dialogs = new List<Dialog>()
                {
                    new Dialog()
                    {
                        Messages = new List<Message>()
                        {
                            _context.Messages.FirstOrDefault(m => m.MessageText == "Text 1"),
                            _context.Messages.FirstOrDefault(m => m.MessageText == "Text 2"),
                        },
                        TicketId = _context.Tickets.FirstOrDefault(t => t.RequestType == "Request 2").Id,
                    },
                };

                _context.Dialogs.AddRange(dialogs);
                _context.SaveChanges();
            }
        }
    }
}