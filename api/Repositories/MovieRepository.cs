
using System.Net;
using api.Data;
using api.Models;

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

        public List<Movie> GetAll()
        {
            //we are going into our DB -> Movies Table and returning all movies
            return _db.Movies.ToList(); 
        }

        public Movie? GetById(int id)
        {
            //find looks up a row by its primary key. Returns null if their is no match
            return _db.Movies.Find(id);    
        }

        public Movie Add(Movie movie)
        {

            //Add only marks the movie as new, Nothing is in the database yet.
            _db.Movies.Add(movie);

            //SaveChanges() sends INSERT to the database. After this, Movie row is filled in.
            _db.SaveChanges();
            return movie; 
        }

        public void Update(Movie movie)
        {
            //We mark the movie as changed, and then save those changes to the DB
           _db.Movies.Update(movie);
           _db.SaveChanges(); 
        }

        public void Delete(Movie movie)
        {
            //Mark the movie for removal, then save the changes to the DB
            _db.Movies.Remove(movie); 
            _db.SaveChanges(); 
        }
    }
}