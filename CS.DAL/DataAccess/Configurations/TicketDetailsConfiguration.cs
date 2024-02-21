using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class TicketDetailsConfiguration : BaseConfiguration<TicketDetails>
    {
        public override void Configure(EntityTypeBuilder<TicketDetails> builder)
        {
            base.Configure(builder);
        }
    }
}
