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
            modelBuilder.Entity<Store>().ToContainer("Stores");
            modelBuilder.Entity<Client>().ToContainer("Clients");
            modelBuilder.Entity<Shoe>().ToContainer("Shoes");
            modelBuilder.Entity<Order>().ToContainer("Orders");
            modelBuilder.Entity<ShoesComment>().ToContainer("ShoesComments");
            modelBuilder.Entity<Courier>().ToContainer("Couriers");
            modelBuilder.Entity<Admin>().ToContainer("Admins");
            modelBuilder.Entity<Category>().ToContainer("Categories");
            modelBuilder.Entity<OrderComment>().ToContainer("OrderComments");

            base.OnModelCreating(modelBuilder);
        }


        DbSet<Client> Clients { get; set; }
        DbSet<Category>  Categories { get; set; }
        DbSet<Admin>  Admins { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<Shoe> Shoes { get; set; }
        DbSet<Order>  Orders { get; set; }
        DbSet<Courier>  Couriers { get; set; }
        DbSet<ShoesComment> ShoesComments { get; set; }
        DbSet<OrderComment> OrderComments   { get; set; }


    }
}
