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
            SeedTickets();
            SeedTicketDetails();
            SeedDialogsWithMessages();
        }

        private Guid userId1 = Guid.Parse("a58f563d-524e-4cba-9c3e-f677cd52006c");
        private Guid userId2 = Guid.Parse("29434c9a-150a-42bf-aa7d-5b726f44fa2c");
        private Guid userId3 = Guid.Parse("737ec30f-6a7c-4729-940d-d7b3021c6d27");
        
        private Guid adminId = Guid.Parse("94483e97-6144-4396-af3e-035673ee3e0f");
        
        private Guid ticketId1 = Guid.Parse("eb2c6e4f-6654-416b-8220-86d6e3914373");
        private Guid ticketId2 = Guid.Parse("52f93f14-67f4-4568-8c5a-67a8a694d47c");
        
        private Guid detailsId1 = Guid.Parse("d673208d-e2fd-4235-a9d0-e838407ab6b9");
        private Guid detailsId2 = Guid.Parse("3ed38a15-56ea-42cd-bfdd-b191b2816b05");
        
        private Guid dialogId1 = Guid.Parse("0d33644f-de19-42a9-b5ec-58c7da96c703");
        
        private Guid messageId1 = Guid.Parse("c48c9e7d-b0c7-4b29-8064-081f55c69621");
        private Guid messageId2 = Guid.Parse("b2b65347-87d4-4e6c-a6aa-556d6bf869fb");
        private Guid messageId3 = Guid.Parse("928077bd-1345-467b-ba79-16e740405b12");

        public void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>()
                {
                    new User() { Id = userId1, Email = "userEmail1@email.com", RoleName = "User", },
                    new User() { Id = userId2, Email = "userEmail2@email.com", RoleName = "User" },
                    new User() { Id = userId3, Email = "userEmail3@email.com", RoleName = "User" },

                    new User() { Id = adminId, Email = "adminEmail1@email.com", RoleName = "Admin" },
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }

        public void SeedTickets()
        {
            if (!_context.Tickets.Any())
            {
                var tickets = new List<Ticket>()
                {
                    new Ticket()
                    {
                        Id = ticketId1,
                        RequestType = "Payment issue",
                        Topic = "I cant buy product",
                        CustomerId = userId1,
                        AdminId = null,
                    },
                    new Ticket()
                    {
                        Id = ticketId2,
                        RequestType = "Web site",
                        Topic = "I found a bug",
                        CustomerId = userId2,
                        AdminId = adminId,
                    }
                };

                _context.Tickets.AddRange(tickets);
                _context.SaveChanges();
            }
        }

        public void SeedTicketDetails()
        {
            if (!_context.TicketDetails.Any())
            {
                var details = new List<TicketDetails>()
                {
                    new TicketDetails()
                    {
                        Id = detailsId1,
                        TicketId = ticketId1,
                        Description = "I wanted to buy product on web site but i doesnt work",
                        CreationTime = DateTime.Now,
                        IsAssigned = false,
                        IsClosed = false,
                        IsSolved = false,
                    },
                    new TicketDetails()
                    {
                        Id = detailsId2,
                        TicketId = ticketId2,
                        Description = "I tried to review my orders buy i cant see them",
                        CreationTime = DateTime.Now,
                        IsAssigned = true,
                        IsClosed = false,
                        IsSolved = false,
                    },
                };

                _context.TicketDetails.AddRange(details);
                _context.SaveChanges();
            }
        }

        public void SeedDialogsWithMessages()
        {
            if (!_context.Dialogs.Any())
            {
                var dialogs = new List<Dialog>()
                {
                    new Dialog()
                    {
                        Id = dialogId1,
                        TicketId = ticketId2,
                    },
                };

                _context.Dialogs.AddRange(dialogs);
                _context.SaveChanges();
            }

            if (!_context.Dialogs.Any(d=>d.Id == dialogId1))
            {
                var messages = new List<Message>()
                {
                    new Message()
                    {
                        Id = messageId1,
                        DialogId = dialogId1,
                        MessageText = "Hello",
                        UserId = adminId,
                        IsRead = true,
                        WhenSend = DateTime.Now,
                    },
                    new Message()
                    {
                        Id = messageId2,
                        DialogId = dialogId1,
                        MessageText = "Hi i need help",
                        UserId = userId2,
                        IsRead = true,
                        WhenSend = DateTime.Now,
                    },
                    new Message()
                    {
                        Id = messageId3,
                        DialogId = dialogId1,
                        MessageText = "Sure can you explain your problem more deeply",
                        UserId = adminId,
                        IsRead = false,
                        WhenSend = DateTime.Now,
                    },
                };

                _context.Messages.AddRange(messages);
                _context.SaveChanges();
            }
        }
    }
}
