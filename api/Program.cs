using api.Data;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

//Registering AppDbContext, so it can be handed to any class that ask for it
//UseSqlite means our database is a single file on disk (app.db)
//The connection string "DatabaseConnection" is read from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"))); 

//Registering our Repository for Dependancy Injection
//Whenever a class asks for IMovieRepository give it our MovieRepository
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieServices, MovieServices>();

builder.Services.AddScoped<IActorRepository, ActorRepository>(); 
builder.Services.AddScoped<IActorServices, ActorServices>(); 

// Everything above register services.Build() creates the app
// Everthing below sets up what happens with each request
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//This sends any http:// request over to https://
app.UseHttpsRedirection();

//Checks [Authorize] attributes. We don't have any yet, but it comes baked in
app.UseAuthorization();

// This connects each controller's routes, like api/movies.
app.MapControllers();

//Start the server and wait for requests
app.Run();
