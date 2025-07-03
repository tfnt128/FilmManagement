using FilmManagement.API.Requests;
using FilmManagement.API.Responses;
using FilmManagement.Shared.Data.Data;
using FilmManagement.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.API.Endpoints
{
    public static class GenresExtension
    {
        public static void MapGenresEndpoints(this WebApplication app)
        {
            app.MapGet("/Genres", ([FromServices] DAL<Genre> dal) => {
                var listOfGenres = dal.List();
                if (listOfGenres is null)
                {
                    return Results.NotFound();
                }
                var listOfGenresResponse = EntityListToResponseList(listOfGenres);
                return Results.Ok(listOfGenresResponse);
            });

            app.MapPost("/Genres", ([FromBody] GenreRequest genreRequest, [FromServices] DAL<Genre> dal) => {
                var genre = new Genre()
                {
                    Name = genreRequest.Name,
                    Description = genreRequest.Description
                };
                dal.Add(genre);
                return Results.Ok(genre);
            });

            app.MapGet("/Genres/{name}", (string name, [FromServices] DAL<Genre> dal) => {
                var genre = dal.Recover(a => a.Name!.ToUpper().Equals(name.ToUpper()));
                if (genre == null)
                {
                    return Results.NotFound($"Genre '{name}' not found.");
                }
                return Results.Ok(EntityToResponse(genre));
            });


            app.MapPut("/Genres", ([FromBody] GenreRequestEdit genreRequestEdit, [FromServices] DAL<Genre> dal) => {
                var genreUpdated = dal.Recover(a => a.Id == genreRequestEdit.Id);
                if (genreUpdated == null)
                {
                    genreUpdated = dal.Recover(a => a.Name!.ToUpper().Equals(genreRequestEdit.Name.ToUpper()));
                    if (genreUpdated == null)
                    {
                        return Results.NotFound($"Genre of ID: {genreRequestEdit.Id} or name '{genreRequestEdit.Name}' not found.");
                    }

                }
                genreUpdated.Name = genreRequestEdit.Name;
                genreUpdated.Description = genreRequestEdit.Description;

                dal.Update(genreUpdated);

                return Results.Ok();
            });

            app.MapDelete("/Genres/{id}", ([FromServices] DAL<Genre> dal, int id) => {
                var genre = dal.Recover(a => a.Id == id);
                if (genre == null)
                {
                    return Results.NotFound($"Genre of ID: '{id}' not found.");
                }
                dal.Delete(genre);
                return Results.Ok(genre);
            });
        }


        private static ICollection<GenreResponse> EntityListToResponseList(IEnumerable<Genre> listOfGenres)
        {
            return listOfGenres.Select(a => EntityToResponse(a)).ToList();
        }

        private static GenreResponse EntityToResponse(Genre genre)
        {
            return new GenreResponse(genre.Id, genre.Name!, genre.Description!);
        }

    }
}
