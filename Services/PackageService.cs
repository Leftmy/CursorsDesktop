using System.Collections.Generic;
using System.Threading.Tasks;
using CursorsDesktop.Abstractions.Services;
using CursorsDesktop.Data;
using CursorsDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CursorsDesktop.Services;

public class PackagesService : IPackagesService
{
    /// <summary>
    /// Gets all packages from the database, including their associated cursors and cursor types.
    /// </summary>
    public async Task<List<PackageModel>> GetAllPackagesAsync()
    {
        using var db = new ApplicationDbContext();
        return await db.Packages
            .AsNoTracking()
            .Include(p => p.Cursors)
            .ThenInclude(c => c.CursorType)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new package to the database.
    /// </summary>
    public async Task<int> AddPackageAsync(PackageModel package)
    {
        using var db = new ApplicationDbContext();
        db.Packages.Add(package);
        await db.SaveChangesAsync();
        return package.Id;
    }

    /// <summary>
    /// Deletes a package from the database by its ID.
    /// All associated cursors will also be deleted due to the cascade delete behavior defined in the model.
    /// </summary>
    /// <param name="packageId">The ID of the package to delete.</param>
    public async Task DeletePackageAsync(int packageId)
    {
        using var db = new ApplicationDbContext();
        var package = await db.Packages.FindAsync(packageId);
        if (package != null)
        {
            db.Packages.Remove(package);
            await db.SaveChangesAsync();
        }
    }
}