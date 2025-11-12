namespace FilmManagement.API.Requests
{
    public record FilmRequestEdit : FilmRequest
    {
        public int Id { get; init; }

        public FilmRequestEdit(
            int id,
            string name,
            int filmmakerId,
            int releaseYear,
            ICollection<GenreRequest> genres)
            : base(name, filmmakerId, releaseYear, genres)
        {
            Id = id;
        }
    }
}
