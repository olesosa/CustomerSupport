using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    public class TicketDetailsConfiguration : IEntityTypeConfiguration<TicketDetails>
    {
        public void Configure(EntityTypeBuilder<TicketDetails> builder)
        {
            builder
                .HasKey(x => x.Id);

        }
    }
}
