using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class MessageAttachmentConfiguration : BaseConfiguration<MessageAttachment>
    {
        public override void Configure(EntityTypeBuilder<MessageAttachment> builder)
        {
            base.Configure(builder);
        }
    }
}
