using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.Tickets)
                .WithOne(u => u.Customer)
                .HasForeignKey(u => u.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(u => u.AssignedTickets)
                .WithOne(u => u.Admin)
                .HasForeignKey(u => u.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasKey(u => u.Id);

            builder
                .HasMany(u => u.Messages)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
