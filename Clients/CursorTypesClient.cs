using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CursorsDesktop.Abstractions.Clients;
using CursorsDesktop.DTO.CursorType;

namespace CursorsDesktop.Clients;

public class CursorTypesClient(HttpClient httpClient) : ICursorTypesClient
{
    private readonly HttpClient _httpClient = httpClient;
    private const string Endpoint = "CursorTypes";

    public async Task<CursorTypesGetAllResponse> GetAllAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<CursorTypesGetAllResponse>(
            Endpoint, CancellationToken.None);
        return data ?? new CursorTypesGetAllResponse([]);
    }
}