using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .HasOne(t => t.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(t => t.TicketId);
        }
    }
}
