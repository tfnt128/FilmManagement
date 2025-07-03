namespace FilmManagement.API.Requests
{
    public record FilmRequestEdit : FilmRequest
    {
        public int Id { get; init; }

        public FilmRequestEdit(int id, string name, int filmmakerId, int releaseYear)
            : base(name, filmmakerId, releaseYear)
        {
            Id = id;
        }
    }
}
