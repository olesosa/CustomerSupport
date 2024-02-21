using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class MessageDetailsConfiguration : BaseConfiguration<MessageDetails>
    {
        public override void Configure(EntityTypeBuilder<MessageDetails> builder)
        {
            base.Configure(builder);
        }
    }
}
