using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ClientShoeShoppingListConfiguration : IEntityTypeConfiguration<ClientShoeShoppingList>
    {
        public void Configure(EntityTypeBuilder<ClientShoeShoppingList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Shoe)
                .WithMany(x => x.ClientShoppingList)
                .HasForeignKey(x => x.ShoeId);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientShoppingList)
                .HasForeignKey(x => x.ClientId);

        }
    }
}
