using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasKey(m => m.Id);
            
            builder
                .HasMany(m=>m.Attachments)
                .WithOne(m=>m.Message)
                .HasForeignKey(m=>m.MessageId);
        }
    }
}
