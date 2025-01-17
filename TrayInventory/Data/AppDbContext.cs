using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrayInventoryApp.Models;

namespace TrayInventoryApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TrayInventorys> TrayInventory { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<DailyRate> DailyRate { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrayInventorys>()
                .HasKey(t => t.TrayInventoryID);  // Explicitly define the primary key
        }
    }
}
