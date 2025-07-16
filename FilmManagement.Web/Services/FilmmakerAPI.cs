using FilmManagement.Web.Requests;
using FilmManagement.Web.Responses;
using System.Net.Http.Json;

public class FilmmakerAPI
{

    private readonly HttpClient _httpClient;
    public FilmmakerAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<FilmmakerResponse>?> GetFilmmakersAsync()
    {
        return await
        _httpClient.GetFromJsonAsync<ICollection<FilmmakerResponse>>("filmmakers");
    }

    public async Task AddFilmmakerAsync(FilmmakerRequest filmmaker)
    {
        await _httpClient.PostAsJsonAsync("filmmakers", filmmaker);
    }

    public async Task DeleteFilmmakerAsync(int id)
    {
        await _httpClient.DeleteAsync($"filmmakers/{id}");
    }
    public async Task UpdateFilmmakerAsync(FilmmakerRequestEdit filmmakerRequestEdit)
    {
        await _httpClient.PutAsJsonAsync("filmmakers", filmmakerRequestEdit);
    }

    public async Task<FilmmakerResponse?> GetFilmmakerNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<FilmmakerResponse>($"filmmakers/{name}");
    }

}
