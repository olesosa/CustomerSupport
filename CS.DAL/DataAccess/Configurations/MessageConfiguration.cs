using CS.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class MessageConfiguration : BaseConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasMany(m=>m.Attachments)
                .WithOne(m=>m.Message)
                .HasForeignKey(m=>m.MessageId);

            base.Configure(builder);
        }
    }
}
