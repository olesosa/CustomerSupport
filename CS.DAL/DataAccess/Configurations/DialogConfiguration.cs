using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS.DAL.DataAccess.Configurations
{
    public class DialogConfiguration : IEntityTypeConfiguration<Dialog>
    {
        public void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder
                .HasKey(d => d.Id);
            
            builder
                .HasOne(d => d.Ticket)
                .WithOne(d => d.Dialog)
                .HasForeignKey<Dialog>(d => d.TicketId);

            builder
                .HasMany(d => d.Messages)
                .WithOne(d => d.Dialog)
                .HasForeignKey(d => d.DialogId);
        }
    }
}
