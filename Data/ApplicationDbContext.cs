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

            modelBuilder.Entity<CursorTypeModel>().HasData(
                new CursorTypeModel { Id = 1, Name = "Default Pointer", SystemRole = "Arrow" },
                new CursorTypeModel { Id = 2, Name = "Help", SystemRole = "Help" },
                new CursorTypeModel { Id = 3, Name = "Working in Background", SystemRole = "AppStarting" },
                new CursorTypeModel { Id = 4, Name = "Busy", SystemRole = "Wait" },
                new CursorTypeModel { Id = 5, Name = "Precision Select", SystemRole = "Crosshair" },
                new CursorTypeModel { Id = 6, Name = "Text Selection", SystemRole = "IBeam" },
                new CursorTypeModel { Id = 7, Name = "Handwriting", SystemRole = "NWPen" },
                new CursorTypeModel { Id = 8, Name = "Not Allowed", SystemRole = "No" },
                new CursorTypeModel { Id = 9, Name = "Resize Vertical", SystemRole = "SizeNS" },
                new CursorTypeModel { Id = 10, Name = "Resize Horizontal", SystemRole = "SizeWE" },
                new CursorTypeModel { Id = 11, Name = "Diagonal Resize 1", SystemRole = "SizeNWSE" },
                new CursorTypeModel { Id = 12, Name = "Diagonal Resize 2", SystemRole = "SizeNESW" },
                new CursorTypeModel { Id = 13, Name = "Move", SystemRole = "SizeAll" },
                new CursorTypeModel { Id = 14, Name = "Alternate Select", SystemRole = "UpArrow" },
                new CursorTypeModel { Id = 15, Name = "Link Select", SystemRole = "Hand" },
                new CursorTypeModel { Id = 16, Name = "Location Select", SystemRole = "Pin" },
                new CursorTypeModel { Id = 17, Name = "Person Select", SystemRole = "Person" },

                new CursorTypeModel { Id = 18, Name = "Auto-scroll (All Directions)", SystemRole = "PanAll" },
                new CursorTypeModel { Id = 19, Name = "Auto-scroll (North-South)", SystemRole = "PanNS" },
                new CursorTypeModel { Id = 20, Name = "Auto-scroll (West-East)", SystemRole = "PanWE" },
                new CursorTypeModel { Id = 21, Name = "Auto-scroll (North)", SystemRole = "PanN" },
                new CursorTypeModel { Id = 22, Name = "Auto-scroll (South)", SystemRole = "PanS" },
                new CursorTypeModel { Id = 23, Name = "Auto-scroll (West)", SystemRole = "PanW" },
                new CursorTypeModel { Id = 24, Name = "Auto-scroll (East)", SystemRole = "PanE" },
                new CursorTypeModel { Id = 25, Name = "Auto-scroll (North West)", SystemRole = "PanNW" },
                new CursorTypeModel { Id = 26, Name = "Auto-scroll (North East)", SystemRole = "PanNE" },
                new CursorTypeModel { Id = 27, Name = "Auto-scroll (South West)", SystemRole = "PanSW" },
                new CursorTypeModel { Id = 28, Name = "Auto-scroll (South East)", SystemRole = "PanSE" },
                new CursorTypeModel { Id = 29, Name = "CD Auto-run", SystemRole = "CD" }
            );
        }
    }
}
