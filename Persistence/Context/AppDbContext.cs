using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Courier>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Store>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Client -> ShoesComment
            modelBuilder.Entity<ShoesComment>()
                .HasOne(sc => sc.Client)
                .WithMany(c => c.ShoesCommnets)
                .HasForeignKey(sc => sc.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Client -> Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Courier -> Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Courier)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CourierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Courier -> OrderComment
            modelBuilder.Entity<OrderComment>()
                .HasOne(oc => oc.Courier)
                .WithMany(c => c.OrderComments)
                .HasForeignKey(oc => oc.CourierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store -> Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store -> Category
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Store)
                .WithMany(s => s.Categorys)
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category -> Shoe
            modelBuilder.Entity<Shoe>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Shoes)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store -> Shoe
            modelBuilder.Entity<Shoe>()
                .HasOne(s => s.Store)
                .WithMany(st => st.Shoes)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            // Shoe -> ClientFavoriteShoes
            modelBuilder.Entity<ClientFavoriShoes>()
                .HasOne(cfs => cfs.Shoe)
                .WithMany(s => s.ClientFavoriShoes)
                .HasForeignKey(cfs => cfs.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Client -> ClientFavoriteShoes
            modelBuilder.Entity<ClientFavoriShoes>()
                .HasOne(cfs => cfs.Client)
                .WithMany(c => c.ClientFavoriShoes)
                .HasForeignKey(cfs => cfs.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Client -> ClientShoeShoppingList
            modelBuilder.Entity<ClientShoeShoppingList>()
                .HasOne(css => css.Client)
                .WithMany(c => c.ClientShoppingList)
                .HasForeignKey(css => css.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Shoe -> ClientShoeShoppingList
            modelBuilder.Entity<ClientShoeShoppingList>()
                .HasOne(css => css.Shoe)
                .WithMany(s => s.ClientShoppingList)
                .HasForeignKey(css => css.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Shoe -> ShoesComment
            modelBuilder.Entity<ShoesComment>()
                .HasOne(sc => sc.Shoe)
                .WithMany(s => s.ShoeComments)
                .HasForeignKey(sc => sc.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> OrderComment
            modelBuilder.Entity<OrderComment>()
                .HasOne(oc => oc.Order)
                .WithOne(o => o.OrderComment)
                .HasForeignKey<OrderComment>(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            // Order -> ShoeCountSize
            modelBuilder.Entity<ShoeCountSize>()
                .HasOne(scs => scs.Order)
                .WithMany(o => o.Shoes)
                .HasForeignKey(scs => scs.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderComment>()
            .HasOne(oc => oc.Client)
            .WithMany(c => c.OrderComments)
            .HasForeignKey(oc => oc.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

            // Shoe -> ShoeCountSize
            modelBuilder.Entity<ShoeCountSize>()
                .HasOne(scs => scs.Shoe)
                .WithMany(s => s.ShoeCountSizes)
                .HasForeignKey(scs => scs.ShoeId)
                .OnDelete(DeleteBehavior.NoAction);



            base.OnModelCreating(modelBuilder);
        }




        public DbSet<Client> Clients { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderComment> OrderComments { get; set; }
        public DbSet<ShoeCountSize> OrderShoeSizeCounts { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<ShoesComment> ShoesComments { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ClientFavoriShoes> ClientFavoriteShoes { get; set; }
        public DbSet<ClientShoeShoppingList> ClientShoeShoppingLists { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
