using System.Collections.Generic;
using System.Threading.Tasks;
using CursorsDesktop.DTO.Packs;

namespace CursorsDesktop.Abstractions.Clients;

public interface IPackageClient
{
    Task<IReadOnlyList<PackagesGetAllResponse>> GetAllAsync();
}