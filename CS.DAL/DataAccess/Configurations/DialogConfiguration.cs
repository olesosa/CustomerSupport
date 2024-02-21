using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class DialogConfiguration : BaseConfiguration<Dialog>
    {
        public override void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder
                .HasOne(d => d.Ticket)
                .WithOne(d => d.Dialog)
                .HasForeignKey<Dialog>(d => d.TicketId);

            builder
                .HasMany(d => d.Messages)
                .WithOne(d => d.Dialog)
                .HasForeignKey(d => d.DialogId);

            base.Configure(builder);
        }
    }
}
