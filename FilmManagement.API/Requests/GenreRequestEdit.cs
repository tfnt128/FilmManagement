namespace FilmManagement.API.Requests
{
    public record class GenreRequestEdit : GenreRequest
    {
        public int Id { get; init; }

        public GenreRequestEdit(int id, string name, string description)
            : base(name, description)
        {
            Id = id;
        }
    }
}
