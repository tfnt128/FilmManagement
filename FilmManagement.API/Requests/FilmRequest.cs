using System.ComponentModel.DataAnnotations;

namespace FilmManagement.API.Requests
{
     public record FilmRequest([Required] string Name, [Required] int FilmmakerId, int ReleaseYear, ICollection<GenreRequest> Genres = null);
}
