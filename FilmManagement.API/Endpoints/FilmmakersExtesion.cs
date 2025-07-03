using FilmManagement.API.Requests;
using FilmManagement.API.Responses;
using FilmManagement.Shared.Data.Data;
using FilmManagement.Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmManagement.API.Endpoints
{
    public static class FilmmakersExtesion
    {
        public static void MapArtistasEndpoints(this WebApplication app)
        {
            app.MapGet("/Filmmakers", ([FromServices] DAL<Filmmaker> dal) => {
                var listOfFilmmakers = dal.List();
                if (listOfFilmmakers is null)
                {
                    return Results.NotFound();
                }
                var listOfFilmmakersResponse = EntityListToResponseList(listOfFilmmakers);
                return Results.Ok(listOfFilmmakersResponse);
            });

            app.MapGet("/Filmmakers/{name}", (string name, [FromServices] DAL<Filmmaker> dal) => {
                var filmmaker = dal.Recover(a => a.Name.ToUpper().Equals(name.ToUpper()));
                if (filmmaker == null)
                {
                    return Results.NotFound($"Filmmaker '{name}' not found.");
                }
                return Results.Ok(EntityToResponse(filmmaker));
            });

            app.MapPost("/Filmmakers", ([FromBody] FilmmakerRequest filmmakerRequest, [FromServices] DAL<Filmmaker> dal) => {
                var filmmaker = new Filmmaker(filmmakerRequest.Name, filmmakerRequest.Bio);
                dal.Add(filmmaker);
                return Results.Ok(filmmaker);
            });

            app.MapDelete("/Filmmakers/{id}", ([FromServices] DAL<Filmmaker> dal, int id) => {
                var filmmaker = dal.Recover(a => a.Id == id);
                if (filmmaker == null)
                {
                    return Results.NotFound($"Filmmaker of ID: '{id}' not found.");
                }
                dal.Delete(filmmaker);
                return Results.Ok(filmmaker);
            });

            app.MapPut("/Filmmakers", ([FromBody] FilmmakerRequestEdit filmmakerRequestEdit, [FromServices] DAL<Filmmaker> dal) => {
                var filmmakerUpdated = dal.Recover(a => a.Id == filmmakerRequestEdit.Id);
                if (filmmakerUpdated == null)
                {
                    filmmakerUpdated = dal.Recover(a => a.Name.ToUpper().Equals(filmmakerRequestEdit.Name.ToUpper()));
                    if (filmmakerUpdated == null)
                    {
                        return Results.NotFound($"Filmmaker of ID: {filmmakerRequestEdit.Id} or name '{filmmakerRequestEdit.Name}' not found.");
                    }

                }
                filmmakerUpdated.Name = filmmakerRequestEdit.Name;
                filmmakerUpdated.Bio = filmmakerRequestEdit.Bio;

                dal.Update(filmmakerUpdated);

                return Results.Ok();
            });
        }

        private static ICollection<FilmmakerResponse> EntityListToResponseList(IEnumerable<Filmmaker> listOfFilmmakers)
        {
            return listOfFilmmakers.Select(a => EntityToResponse(a)).ToList();
        }

        private static FilmmakerResponse EntityToResponse(Filmmaker filmmaker)
        {
            return new FilmmakerResponse(filmmaker.Id, filmmaker.Name, filmmaker.Bio);
        }
    }
}
