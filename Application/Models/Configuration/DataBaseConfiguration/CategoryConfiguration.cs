using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
                

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Categorys)
                .HasForeignKey(x => x.StoreId)
                .IsRequired();
        }
    }
}
