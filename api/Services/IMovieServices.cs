using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
//List of jobs that our services must impliment
//Our services speak in DTO. The controller never touches the entity
namespace api.Services
{
    public interface IMovieServices
    {
        List<MovieResponseDto> GetAll();
        MovieResponseDto? GetById(int id); 
        MovieResponseDto Create(CreateMovieDto dto);

        //This will return null if the movie does not exist
        MovieResponseDto? Update(int id, UpdateMovieDto dto); 

        //returns true if the movie is deleted
        bool Delete(int id);  
    }
}