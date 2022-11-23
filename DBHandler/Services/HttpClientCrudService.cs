using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBHandler.Services
{
    /// <summary>
    /// Create new HttpClient, initialize it and add configuration in a constructor
    /// </summary>
    public class HttpClientCrudService : IHttpClientServiceImplementation
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly JsonSerializerOptions _options;

        public HttpClientCrudService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7270/swagger/index.html"); //new Uri("https://localhost:44349/api/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task Execute() 
        {
            await GetGroups();
        }

        public async Task GetGroups()
        {
            var response = await _httpClient.GetAsync("tblGroup");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            //var groups = JsonSerializer.Deserialize<List<GroupDto>>(content, _options);
        }
    }
}
