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
        public MovieResponseDto Create(CreateMovieDto dto)
        {
                Movie movie = new Movie(); 
                movie.Title = dto.Title; 
                movie.Director = dto.Director; 
                movie.Rating = dto.Rating; 
                movie.ReleaseYear = dto.ReleaseYear; 

             Movie saved = _repository.Add(movie); 
            //Sending back the same movie, but now with an IdrestaurantControllerController
            return ToDto(saved); 
        }

        public List<MovieResponseDto> GetAll()
        {
            List<Movie> movies = _repository.GetAll();

            return movies.Select(ToDto).ToList();
        }

        public MovieResponseDto? GetById(int id)
        {
            Movie? movie = _repository.GetById(id);
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

        public MovieResponseDto? Update(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? entity = _repository.GetById(id); 
           
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

            _repository.Update(entity); 

            return ToDto(entity); 
            
        }

        public bool Delete(int id)
        {
           Movie? movie = _repository.GetById(id); 
           if(movie is null)
            {
                //False tells our controller that no movie was found
                //Send a 404
                return false; 
            }
            _repository.Delete(movie); 
            return true; 
        }
    }
}