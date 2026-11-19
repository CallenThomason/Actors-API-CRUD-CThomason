
using System.Net;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    //The repository layer: The ONLY later that talks to the database. 
    //It uses AppDbContext (Ef Core) to read and save Movie Entities
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db; 

        //DI: We never write a "new AppDbContext()" ourselves
        //ASP.NET Core sees this constructor asks for one and passes it in for us. 
        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        //async lets the method use await inside of it
        public async Task<List<Movie>> GetAllAsync()
        {
            //we are going into our DB -> Movies Table and returning all movies
            //await: while DB is working, this thread is freed to handle other requests
            return await _db.Movies.ToListAsync(); 
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            //FindAsync looks up a row by its primary key. Returns null if their is no match
            return await _db.Movies.FindAsync(id);    
        }

        public async Task<Movie> AddAsync(Movie movie)
        {

            //Add only marks the movie as new, Nothing is in the database yet.
            //Add had no await: It does not talk to the database
            _db.Movies.Add(movie);

            //SaveChanges() sends INSERT to the database. After this, Movie row is filled in.
           await _db.SaveChangesAsync();
            return movie; 
        }

        public async Task UpdateAsync(Movie movie)
        {
            //We mark the movie as changed, and then save those changes to the DB
           _db.Movies.Update(movie);
          await _db.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(Movie movie)
        {
            //Mark the movie for removal, then save the changes to the DB
            _db.Movies.Remove(movie); 
            await _db.SaveChangesAsync(); 
        }
    }
}