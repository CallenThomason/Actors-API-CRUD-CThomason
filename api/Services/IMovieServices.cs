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
        //Every method returns a Task, so the caller can await it
        //Task<bool> means a bool will be ready later
        Task<List<MovieResponseDto>> GetAllAsync();
        Task<MovieResponseDto?> GetByIdAsync(int id); 
        Task<MovieResponseDto> CreateAsync(CreateMovieDto dto);

        //This will return null if the movie does not exist
        Task<MovieResponseDto?> UpdateAsync(int id, UpdateMovieDto dto); 

        //returns true if the movie is deleted
        Task<bool> DeleteAsync(int id);  
    }
}