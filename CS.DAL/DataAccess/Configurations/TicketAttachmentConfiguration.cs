using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder
                .HasKey(a => a.Id);
            
            builder
                .HasOne(t => t.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(t => t.TicketId);
        }
    }
}
