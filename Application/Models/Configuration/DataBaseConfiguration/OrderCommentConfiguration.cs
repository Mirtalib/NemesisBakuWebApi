using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class OrderCommentConfiguration : IEntityTypeConfiguration<OrderComment>
    {
        public void Configure(EntityTypeBuilder<OrderComment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.Rate);

            builder.HasOne(x => x.Order)
                .WithOne(order => order.OrderComment)
                .HasForeignKey<OrderComment>(x => x.OrderId)
                .IsRequired();

            builder.HasOne(x => x.Client)
                .WithMany(x => x.OrderComments)
                .HasForeignKey(x => x.ClientId);

            builder.HasOne(x => x.Courier)
                .WithMany(x=> x.OrderComments)
                .HasForeignKey(x => x.CourierId);
        }
    }
}
