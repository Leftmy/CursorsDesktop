using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CursorsDesktop.Abstractions.Clients;
using CursorsDesktop.DTO.Packs;

namespace CursorsDesktop.Clients;

public class PackagesClient(HttpClient httpClient) : IPackageClient
{
    private readonly HttpClient _httpClient = httpClient;
    private const string Endpoint = "Packages";

    public async Task<IReadOnlyList<PackagesGetAllResponse>> GetAllAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<IReadOnlyList<PackagesGetAllResponse>>(
            Endpoint + "/all", CancellationToken.None
        );

        return data ?? [];
    }
}