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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x=>x.Addresses)
                .IsRequired();
            

        }

    }
}
