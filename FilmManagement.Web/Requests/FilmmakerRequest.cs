using System.ComponentModel.DataAnnotations;

namespace FilmManagement.Web.Requests
{
    public record FilmmakerRequest([Required] string Name, [Required] string bio, string? image);
}
