using CS.DAL.DataAccess.Configurations;
using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.DAL.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketAttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new TicketDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DialogConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageAttachmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
