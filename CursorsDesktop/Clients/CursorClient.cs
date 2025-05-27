using CursorsDesktop.DTO;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace CursorsDesktop.Clients
{
    public class CursorClient
    {
        private readonly HttpClient _httpClient;

        public CursorClient(string baseUrl = "http://localhost:5084")
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<CursorDTO> GetCursorById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var cursor = await response.Content.ReadFromJsonAsync<CursorDTO>();
            return cursor;
        }

    }
}

