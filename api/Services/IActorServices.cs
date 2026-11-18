

using api.Dtos;
using api.Models;

namespace api.Services
{
    public interface IActorServices
    {
         List<ActorResponseDto> GetAll(); 
        ActorResponseDto? GetById(int id);
        ActorResponseDto Create(CreateActorDto dto); 
        ActorResponseDto Update(int id, UpdateActorDto dto); 

        bool Delete(int id); 
        bool Exists(CreateActorDto dto); 
        bool UpdateExists(UpdateActorDto dto); 

    }
}