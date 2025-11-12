namespace FilmManagement.Web.Responses
{
    public record FilmResponse(
        int Id,
        string Name,
        int FilmmakerId,
        string FilmmakerName,
        int ReleaseYear,
        IEnumerable<string>? Genres = null
    );
}
