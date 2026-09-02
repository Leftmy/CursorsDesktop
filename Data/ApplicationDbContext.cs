using System;
using System.IO;
using CursorsDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CursorsDesktop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);

                var appFolder = Path.Combine(path, AppConstants.DatabaseFolderName);
                Directory.CreateDirectory(appFolder);
                var dbPath = Path.Combine(appFolder, AppConstants.DatabaseFileName);

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        public DbSet<CursorModel> Cursors { get; set; } = default!;
        public DbSet<CursorTypeModel> CursorTypes { get; set; } = default!;
        public DbSet<PackageModel> Packages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PackageModel>(entity =>
            {
                entity.HasIndex(e => e.PackageName).IsUnique();
            });

            modelBuilder.Entity<CursorTypeModel>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.SystemRole).IsUnique();
            });

            modelBuilder.Entity<CursorModel>(entity =>
            {
                entity.HasOne(c => c.CursorType)
                      .WithMany(ct => ct.Cursors)
                      .HasForeignKey(c => c.TypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Package)
                      .WithMany(p => p.Cursors)
                      .HasForeignKey(c => c.PackageId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
