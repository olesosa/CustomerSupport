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
                .HasForeignKey(u => u.CustomerId);

            builder
                .HasMany(u => u.Tickets)
                .WithOne(u => u.Admin)
                .HasForeignKey(u => u.AdminId);

            builder
                .HasKey(u => u.Id);
        }
    }
}
