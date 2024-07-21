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

            builder.HasMany(x => x.ClientShoppingList)
                .WithMany(x => x.ClientShoppingList);
        }
    }
}
