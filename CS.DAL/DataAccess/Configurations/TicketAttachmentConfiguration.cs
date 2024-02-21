using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class TicketAttachmentConfiguration : BaseConfiguration<TicketAttachment>
    {
        public override void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder
                .HasOne(t => t.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(t => t.TicketId);

            base.Configure(builder);
        }
    }
}
