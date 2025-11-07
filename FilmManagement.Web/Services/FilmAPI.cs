
using FilmManagement.Web.Requests;
using FilmManagement.Web.Responses;
using System.Net.Http.Json;

public class FilmAPI
{
    private readonly HttpClient _httpClient;
    public FilmAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }
    public async Task<ICollection<FilmResponse>?> GetFilmsAsync()
    {
        return await
        _httpClient.GetFromJsonAsync<ICollection<FilmResponse>>("films");
    }

    public async Task AddFilmAsync(FilmRequest film)
    {
        await _httpClient.PostAsJsonAsync("films", film);
    }

    public async Task DeleteFilmAsync(int id)
    {
        await _httpClient.DeleteAsync($"films/{id}");
    }

    public async Task<FilmResponse?> GetFilmByNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<FilmResponse>($"films/{name}");
    }

}

