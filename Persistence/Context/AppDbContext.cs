using Application.Models.Configuration.DataBaseConfiguration;
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
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourierConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderCommentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderShoeSizeCountConfiguration());
            modelBuilder.ApplyConfiguration(new ShoeConfiguration());
            modelBuilder.ApplyConfiguration(new ShoeCountSizeConfiguration());
            modelBuilder.ApplyConfiguration(new ShoeCommentConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());

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
