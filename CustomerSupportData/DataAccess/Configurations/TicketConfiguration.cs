using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasOne(t => t.Customer)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.CustomerId);

            builder
                .HasOne(t => t.Admin)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.AdminId);

            builder
                .HasKey(t => t.Id);

            builder
                .HasMany(t => t.Attachments)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t => t.Id);

            builder
                .HasOne(t => t.Details)
                .WithOne(t => t.Ticket)
                .HasForeignKey<TicketDetails>(t => t.Id);

            builder
                .HasOne(t => t.Dialog)
                .WithOne(t => t.Ticket)
                .HasForeignKey<Dialog>(d => d.TicketId);

        }
    }
}
