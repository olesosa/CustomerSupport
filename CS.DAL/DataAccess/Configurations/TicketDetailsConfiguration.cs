using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class TicketDetailsConfiguration : IEntityTypeConfiguration<TicketDetails>
    {
        public void Configure(EntityTypeBuilder<TicketDetails> builder)
        {
            builder
                .HasKey(d => d.Id);
        }
    }
}
