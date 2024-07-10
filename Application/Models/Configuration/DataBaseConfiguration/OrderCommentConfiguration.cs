using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Models.Configuration.DataBaseConfiguration
{
    public class OrderCommentConfiguration : IEntityTypeConfiguration<OrderComment>
    {
        public void Configure(EntityTypeBuilder<OrderComment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
