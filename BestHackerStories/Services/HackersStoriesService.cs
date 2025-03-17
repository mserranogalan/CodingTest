using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BestHackerStories.Responses;
using System.ComponentModel;

namespace BestHackerStories.Services
{
    public class HackersStoriesService
    {
        private readonly HttpClient _httpClient;
        public HackersStoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> GetAsync()
        {
            var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/beststories.json");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<int>>(jsonResponse);
        }

        public async Task<BestHackersStories> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BestHackersStories>(jsonResponse);
        }
    }

}
