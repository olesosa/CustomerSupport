using CS.DAL.Models;
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
                .HasForeignKey<Ticket>(t => t.DetailsId);

            base.Configure(builder);
        }
    }
}
