﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ShoeCommentConfiguration : IEntityTypeConfiguration<ShoesComment>
    {
        public void Configure(EntityTypeBuilder<ShoesComment> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.Rate);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.ShoesCommnets)
                .HasForeignKey(x => x.ClientId);

            builder.HasOne(x => x.Shoe)
                .WithMany(x => x.ShoeComments)
                .HasForeignKey(x => x.ShoeId);
        }
    }
}
