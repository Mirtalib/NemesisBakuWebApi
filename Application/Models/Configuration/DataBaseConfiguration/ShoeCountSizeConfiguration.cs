using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ShoeCountSizeConfiguration : IEntityTypeConfiguration<ShoeCountSize>
    {
        public void Configure(EntityTypeBuilder<ShoeCountSize> builder)
        {
            throw new NotImplementedException();
        }
    }
}
