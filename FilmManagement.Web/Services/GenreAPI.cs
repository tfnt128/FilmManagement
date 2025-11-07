using FilmManagement.Web.Responses;
using System.Net.Http.Json;


    public class GenreAPI
    {
        private readonly HttpClient _httpClient;

        public GenreAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<List<GenreResponse>?> GetGenresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GenreResponse>>("genres");
        }
        public async Task<GenreResponse?> GetGeneroPorNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<GenreResponse>($"genres/{name}");
        }
    }

