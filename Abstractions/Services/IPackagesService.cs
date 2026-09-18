using System.Collections.Generic;
using System.Threading.Tasks;
using CursorsDesktop.Models;

namespace CursorsDesktop.Abstractions.Services;

public interface IPackagesService
{
    Task<List<PackageModel>> GetAllPackagesAsync();
    Task<int> AddPackageAsync(PackageModel package);
    Task DeletePackageAsync(int packageId);
}