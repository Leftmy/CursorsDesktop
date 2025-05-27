using CursorsDesktop.DTO;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace CursorsDesktop.Clients
{
    public class PackageClient
    {
        private readonly HttpClient _httpClient;

        public PackageClient(string baseUrl = "http://localhost:5084")
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<PackageDTO> GetPackageByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/packages/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var package = await response.Content.ReadFromJsonAsync<PackageDTO>();
            return package;
        }

        public async Task<ObservableCollection<PackageDTO>> GetAllPackages()
        {
            var response = await _httpClient.GetAsync($"/api/packages/all");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var package = await response.Content.ReadFromJsonAsync<ObservableCollection<PackageDTO>>();
            return package;
        }

    }
}

