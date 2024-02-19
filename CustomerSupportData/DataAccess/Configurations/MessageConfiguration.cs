using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(m => m.Details)
                .WithOne(m => m.Message)
                .HasForeignKey<Message>(m => m.DetailsId);

            builder
                .HasMany(m=>m.Attachments)
                .WithOne(m=>m.Message)
                .HasForeignKey(m=>m.MessageId);
        }
    }
}
