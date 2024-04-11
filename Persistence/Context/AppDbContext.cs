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
            //modelBuilder.Entity<Client>().ToContainer("Clients");
            //modelBuilder.Entity<Store>().ToContainer("Stores");
            //modelBuilder.Entity<Shoes>().ToContainer("Shoes");
            //modelBuilder.Entity<Order>().ToContainer("Orders");
            //modelBuilder.Entity<ShoesComment>().ToContainer("ShoesComments");
            //modelBuilder.Entity<Courier>().ToContainer("Couriers");

            base.OnModelCreating(modelBuilder);
        }


        DbSet<Client> Clients { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<Shoes> Shoes { get; set; }
        DbSet<Order>  Orders { get; set; }
        DbSet<Courier>  Couriers { get; set; }
        DbSet<ShoesComment> ShoesComments { get; set; }


    }
}
