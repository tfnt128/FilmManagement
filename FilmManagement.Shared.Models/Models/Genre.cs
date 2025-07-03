namespace FilmManagement.Shared.Models.Models
{
    public class Genre
    {
        public virtual ICollection<Films> Films { get; set; } = new List<Films>();
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
