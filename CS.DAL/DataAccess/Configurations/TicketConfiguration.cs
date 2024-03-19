using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class TicketConfiguration : BaseConfiguration<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasOne(t => t.Details)
                .WithOne(t => t.Ticket)
                .HasForeignKey<TicketDetails>(t => t.TicketId);

            builder
                .Property(t => t.Number)
                .UseIdentityColumn(1);

            base.Configure(builder);
        }
    }
}
