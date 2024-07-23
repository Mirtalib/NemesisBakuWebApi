using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.OrderFinishTime)
                .IsRequired();

            builder.Property(x => x.OrderMakeTime)
                .IsRequired();

            builder.Property(x => x.OrderStatus)
                .IsRequired();


            builder.HasOne(x => x.Client)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ClientId);


            builder.HasOne(x => x.Store)
                .WithMany(x =>x.Orders)
                .HasForeignKey(x => x.StoreId);
            
            builder.HasOne(x => x.Courier)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CourierId);

            builder.HasOne(x => x.OrderComment)
                .WithOne(x=> x.Order)
                .HasForeignKey<OrderComment>(x => x.OrderId)
                .IsRequired();

        }
    }
}
