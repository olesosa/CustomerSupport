﻿using CustomerSupportData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportData.DataAccess.Configurations
{
    public class MessageDetailsConfiguration : IEntityTypeConfiguration<MessageDetails>
    {
        public void Configure(EntityTypeBuilder<MessageDetails> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(m => m.Message)
                .WithOne(m => m.Details)
                .HasForeignKey<Message>(m => m.DetailsId);
        }
    }
}
