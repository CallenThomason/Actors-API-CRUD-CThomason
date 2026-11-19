//Our Services Layer is the middle man between our Controller and Repository
//It maps our Dtos to Entities and back. This is where our logic happens

using api.Dtos;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _repository;

        //DI: In program.cs says IMovieRepository mean MovieRepository
        public MovieServices(IMovieRepository repository)
        {
            _repository = repository;
        }
        public async Task<MovieResponseDto> CreateAsync(CreateMovieDto dto)
        {
                Movie movie = new Movie(); 
                movie.Title = dto.Title; 
                movie.Director = dto.Director; 
                movie.Rating = dto.Rating; 
                movie.ReleaseYear = dto.ReleaseYear; 

             Movie saved = await _repository.AddAsync(movie); 
            //Sending back the same movie, but now with an IdrestaurantControllerController
            return ToDto(saved); 
        }

        //This meth awaits the repository layer, so it HAS to be async and return a Task 
        //Once the bottom layer is async, every later that calls it MUST be async as well
        public async Task<List<MovieResponseDto>> GetAllAsync()
        {
            //Await waits for the database without blocking the thread
            List<Movie> movies = await _repository.GetAllAsync();

            //Turns every Movie entity into a MovieResponseDto
            return movies.Select(ToDto).ToList();
        }

        public async Task<MovieResponseDto?> GetByIdAsync(int id)
        {
            Movie? movie = await _repository.GetByIdAsync(id);
            //If the movie does not exist pass movies to the controller
            if (movie is null)
            {
                return null;
            }
            return ToDto(movie);
        }

        //We will Create a Helper Function that maps our Dtos
        //Private because it doesn't need to be accessed ourside of our class
        //static because it doesn't have access to instance data
        private static MovieResponseDto ToDto(Movie movie)
        {
            MovieResponseDto dto = new MovieResponseDto();
            // dto.Id = movie.Id; 
            // dto.Director = movie.Director; 
            // dto.Rating = movie.Rating; 
            // dto.ReleaseYear = movie.ReleaseYear; 
            // dto.Title = movie.Title; 



            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Rating = movie.Rating,
                ReleaseYear = movie.ReleaseYear

            };
        }

        public async Task<MovieResponseDto?> UpdateAsync(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? entity = await _repository.GetByIdAsync(id); 
           
           //If our id is not in the data base we return null
            if(entity is null)
            {
                return null; 
            }

            entity.Title = dto.Title; 
            entity.Director = dto.Director; 
            entity.Rating = dto.Rating; 
            entity.ReleaseYear = dto.ReleaseYear; 
            //Records when this movie is last changed
            entity.UpdatedAt = DateTime.UtcNow;  

          await  _repository.UpdateAsync(entity); 

            return ToDto(entity); 
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
           Movie? movie = await _repository.GetByIdAsync(id); 
           if(movie is null)
            {
                //False tells our controller that no movie was found
                //Send a 404
                return false; 
            }
            await _repository.DeleteAsync(movie); 
            return true; 
        }
    }
}