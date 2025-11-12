using FilmManagement.API.Requests;
using FilmManagement.API.Responses;
using FilmManagement.Shared.Data.Data;
using FilmManagement.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.API.Endpoints
{
    public static class FilmsExtension
    {
        public static void MapFilmsEndpoints(this WebApplication app)
        {
            app.MapGet("/Films", ([FromServices] DAL<Films> dal) => {
                var listOfFilms = dal.List();
                if (listOfFilms is null)
                {
                    return Results.NotFound();
                }
                var listOfFilmsResponse = EntityListToResponseList(listOfFilms);
                return Results.Ok(listOfFilmsResponse);
            });

            app.MapGet("/Films/{name}", (string name, [FromServices] DAL<Films> dal) => {
                var film = dal.Recover(a => a.Name.ToUpper().Equals(name.ToUpper()));
                if (film == null)
                {
                    return Results.NotFound($"Film '{name}' not found.");
                }
                return Results.Ok(EntityToResponse(film));
            });

            app.MapPost("/Films", ([FromBody] FilmRequest filmRequest, [FromServices] DAL<Films> dalFilm,
                [FromServices] DAL<Genre> dalGenre) => {
                    var film = new Films(filmRequest.Name)
                    {
                        FilmmakerId = filmRequest.FilmmakerId,
                        ReleaseYear = filmRequest.ReleaseYear,
                        Genres = filmRequest.Genres is not null ?
                        GenreRequestConverter(filmRequest.Genres, dalGenre) : new List<Genre>()
                    };
                    dalFilm.Add(film);
                    return Results.Ok(film);
                });

            app.MapDelete("/Films/{id}", ([FromServices] DAL<Films> dal, int id) => {
                var film = dal.Recover(a => a.Id == id);
                if (film == null)
                {
                    return Results.NotFound($"Film of ID: '{id}' not found.");
                }
                dal.Delete(film);
                return Results.Ok(film);
            });

            app.MapPut("/Films", ([FromBody] FilmRequestEdit filmRequestEdit, [FromServices] DAL<Films> dal) => {
                var filmUpdated = dal.Recover(a => a.Id == filmRequestEdit.Id);
                if (filmUpdated == null)
                {
                    filmUpdated = dal.Recover(a => a.Name.ToUpper().Equals(filmRequestEdit.Name.ToUpper()));
                    if (filmUpdated == null)
                    {
                        return Results.NotFound($"Film of ID: {filmRequestEdit.Id} and Name: '{filmRequestEdit.Name}' not found.");
                    }

                }
                filmUpdated.Name = filmRequestEdit.Name;
                filmUpdated.ReleaseYear = filmRequestEdit.ReleaseYear;

                dal.Update(filmUpdated);

                return Results.Ok();
            });
        }

        private static ICollection<Genre> GenreRequestConverter(ICollection<GenreRequest> genres, DAL<Genre> dalGenre)
        {
            var listOfGenres = new List<Genre>();
            foreach (var item in genres)
            {
                var entity = EntityToRequest(item);
                var genre = dalGenre.Recover(a => a.Name!.ToUpper().Equals(item.Name.ToUpper()));
                if (genre != null)
                    listOfGenres.Add(genre);
                else
                    listOfGenres.Add(entity);
            }

            return listOfGenres;
        }

        private static ICollection<FilmResponse> EntityListToResponseList(IEnumerable<Films> listOfFilms)
        {
            return listOfFilms.Select(a => EntityToResponse(a)).ToList();
        }

        private static FilmResponse EntityToResponse(Films film)
        {
            return new FilmResponse(
                film.Id,
                film.Name!,
                film.Filmmaker!.Id,
                film.Filmmaker.Name,
                film.ReleaseYear ?? 0,  
                film.Genres?.Select(g => g.Name).ToList() ?? new List<string>()
            );
        }
        
        private static Genre EntityToRequest(GenreRequest genre)
        {
            return new Genre()
            {
                Name = genre.Name,
                Description = genre.Description,
            };
        }
    }
}
