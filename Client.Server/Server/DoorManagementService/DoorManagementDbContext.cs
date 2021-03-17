using Microsoft.EntityFrameworkCore;
using System;

namespace DoorManagementService
{
    public class DoorManagementDbContext : DbContext
    {

        public static readonly DoorManagementDbContext Instance = new DoorManagementDbContext();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=" + AppDomain.CurrentDomain.BaseDirectory + "DoorManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Door>();
            modelBuilder.Entity<LockStatusMaster>();
            modelBuilder.Entity<OpenStatusMaster>();
        }

        public DbSet<Door> DoorDbSet { get; set; }

        public DbSet<LockStatusMaster> LockStatusDbSet { get; set; }

        public DbSet<OpenStatusMaster> OpenStatusDbSet { get; set; }

    }
}
