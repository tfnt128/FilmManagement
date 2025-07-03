using FilmManagement.API.Endpoints;
using FilmManagement.Shared.Data.Data;
using FilmManagement.Shared.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FilmManagementContext>((options) =>
    options
    .UseSqlServer(builder.Configuration["ConnectionStrings:FilmMakerDB"])
    .UseLazyLoadingProxies()
);
builder.Services.AddTransient<DAL<Filmmaker>>();
builder.Services.AddTransient<DAL<Films>>();
builder.Services.AddTransient<DAL<Genre>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure
    <Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler
    = ReferenceHandler.IgnoreCycles);
var app = builder.Build();


app.MapArtistasEndpoints();
app.MapFilmsEndpoints();
app.MapGenresEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
