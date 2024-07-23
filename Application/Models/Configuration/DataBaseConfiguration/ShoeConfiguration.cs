using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ShoeConfiguration : IEntityTypeConfiguration<Shoe>
    {
        public void Configure(EntityTypeBuilder<Shoe> builder)
        {

            builder.HasKey(x => x.Id);


            builder.Property(x => x.Model)
                .IsRequired();

            builder.Property(x => x.Brend)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(); 

            builder.Property(x => x.ImageUrls)
                .IsRequired(); 

            builder.Property(x => x.Color)
                .IsRequired(); 

            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Shoes)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Shoes)
                .HasForeignKey(x => x.StoreId);


        }
    }
}
