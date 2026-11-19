

using api.Dtos;
using api.Models;

namespace api.Services
{
    public interface IActorServices
    {
         Task<List<ActorResponseDto>> GetAllAsync(); 
        Task<ActorResponseDto?> GetByIdAsync(int id);
       Task<ActorResponseDto> CreateAsync(CreateActorDto dto); 
       Task<ActorResponseDto> UpdateAsync(int id, UpdateActorDto dto); 

       Task<bool> DeleteAsync(int id); 
       Task<bool> ExistsAsync(CreateActorDto dto); 
       Task<bool> UpdateExistsAsync(UpdateActorDto dto); 

    }
}