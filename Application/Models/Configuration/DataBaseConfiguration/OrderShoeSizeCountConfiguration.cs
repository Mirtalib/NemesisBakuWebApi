using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class OrderShoeSizeCountConfiguration : IEntityTypeConfiguration<OrderShoeSizeCount>
    {
        public void Configure(EntityTypeBuilder<OrderShoeSizeCount> builder)
        {
            throw new NotImplementedException();
        }
    }
}
