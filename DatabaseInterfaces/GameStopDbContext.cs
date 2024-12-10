using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace DatabaseInterfaces
{
    public class GameStopDbContext : DbContext
    {
        public DbSet<Product> product { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Inventory> inventory { get; set; }
        public DbSet<Transaction> transaction { get; set; }
        public DbSet<TransactionItem> transactionItem { get; set; }
        public DbSet<Store> store { get; set; }
        public DbSet<Vendor> vendor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseMySql(
                "Server=localhost;Database=gamestop;User=root;Password=BabyChops$!;",
                new MySqlServerVersion(new Version(8, 0, 23)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //composite primary key for TransactionItem
            modelBuilder.Entity<TransactionItem>()
                .HasKey(ti => new { ti.TransactionID, ti.UPC }); //composite key using TransactionID and UPC

            //defining relationships
            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionItems)  //assuming Transaction has a collection of TransactionItems
                .HasForeignKey(ti => ti.TransactionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Product)
                .WithMany()
                .HasForeignKey(ti => ti.UPC);  //assuming UPC is a unique identifier for Product

            //composite primary key for Inventory
            modelBuilder.Entity<Inventory>()
                .HasKey(i => new { i.StoreID, i.UPC });

            //define foreign key relationships
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Store)
                .WithMany()
                .HasForeignKey(i => i.StoreID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.UPC)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
