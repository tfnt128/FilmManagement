using System.ComponentModel.DataAnnotations;

namespace FilmManagement.Shared.Models.Models
{
    public class Films
    {
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public Films() { }
        public Films(string name)
        {
            Name = name;
        }

        [Required] public string Name { get; set; }
        public int Id { get; set; }
        public int? ReleaseYear { get; set; }
        public int? FilmmakerId { get; set; }
        public virtual Filmmaker? Filmmaker { get; set; }
    }
}
