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
    public class ClientFavoriShoesConfiguration : IEntityTypeConfiguration<ClientFavoriShoes>
    {
        public void Configure(EntityTypeBuilder<ClientFavoriShoes> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientFavoriShoes)
                .HasForeignKey(x => x.ClientId);

            builder.HasOne(x => x.Shoe)
                .WithMany(x => x.ClientFavoriShoes)
                .HasForeignKey(x => x.ShoeId);
        }
    }
}
