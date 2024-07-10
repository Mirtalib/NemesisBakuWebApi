using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class ShoeCommentConfiguration : IEntityTypeConfiguration<ShoesComment>
    {
        public void Configure(EntityTypeBuilder<ShoesComment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
