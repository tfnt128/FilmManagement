namespace FilmManagement.Web.Requests
{
    public record FilmmakerRequestEdit : FilmmakerRequest
    {
        public int Id { get; init; }

        public FilmmakerRequestEdit(int id, string name, string bio)
            : base(name, bio)
        {
            Id = id;
        }
    }
}
