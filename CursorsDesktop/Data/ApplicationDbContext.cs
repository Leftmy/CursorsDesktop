using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursorsDesktop.Entities;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using CursorType = CursorsDesktop.Entities.CursorType;
using Microsoft.Extensions.DependencyInjection;


namespace CursorsDesktop.Data
{
    class ApplicationDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = app.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>().HasIndex(b => b.PackageName).IsUnique(true);
            modelBuilder.Entity<Cursor>().HasIndex(b => new { b.CursorName, b.CursorTypeId, b.PackageId}).IsUnique(true);
            modelBuilder.Entity<CursorType>().HasIndex(b => b.type).IsUnique(true);
        }


        public DbSet<Cursor> Cursors { get; set; } = null!;
        public DbSet<CursorType> CursorTypes { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;

    }



}
