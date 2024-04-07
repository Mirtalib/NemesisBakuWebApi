using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>().ToContainer("Clients");
            modelBuilder.Entity<ShoeStore>().ToContainer("ShoeStores");
            modelBuilder.Entity<Shoes>().ToContainer("Shoes");
            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<ShoesComment>().ToContainer("ShoesComments");

            base.OnModelCreating(modelBuilder);
        }


        DbSet<Client> Clients { get; set; }
        DbSet<ShoeStore> ShoeStores { get; set; }
        DbSet<Shoes> Shoes { get; set; }
        DbSet<Order>  Orders { get; set; }
        DbSet<ShoesComment> ShoesComments { get; set; }


    }
}
