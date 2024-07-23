using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Name)
                .IsRequired();

            builder.Property(x=>x.Description)
                .IsRequired();  

            builder.Property(x=>x.Email)
                .IsRequired();    

            builder.Property(x=>x.PhoneNumbers)
                .IsRequired();   
            
            builder.Property(x=>x.Addresses)
                .IsRequired();

            

        }

    }
}