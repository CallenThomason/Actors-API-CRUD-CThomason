

using api.Models;

namespace api.Repositories
{
    public interface IMovieRepository
    {
        //The repository interface: The leist of database jobs for movies. 
        //Repositories work with entities (movie), Never DTOs
        // List<Movie> GetAll(); 
        //Task<T> means "A T that will be ready Later", once the DB answers
        //The Async at the end of the name tells the caller to await it. 
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id); 
        //Returns one movie, or null if no movie has that id (That's what the ? means). 
        Task<Movie> AddAsync(Movie movie); 

        //a plain Task (No <T>) means no value comes back, but you can still await it. 
        Task UpdateAsync(Movie movie);

        Task DeleteAsync(Movie movie); 
    }
}