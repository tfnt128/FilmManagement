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
        _httpClient.GetFromJsonAsync<ICollection<FilmmakerResponse>>("Filmmakers");
    }
}
