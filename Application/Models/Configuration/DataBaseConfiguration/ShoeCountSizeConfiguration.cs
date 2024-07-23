using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ShoeCountSizeConfiguration : IEntityTypeConfiguration<ShoeCountSize>
    {
        public void Configure(EntityTypeBuilder<ShoeCountSize> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Count)
                .IsRequired();

            builder.Property(x => x.Size)
                .IsRequired();


            builder.HasOne(x => x.Order)
                .WithMany(x => x.Shoes)
                .HasForeignKey(x => x.OrderId);


            builder.HasOne(x => x.Shoe)
                .WithMany(x => x.ShoeCountSizes)
                .HasForeignKey(x => x.ShoeId);

        }
    }
}
