

using api.Dtos;
using api.Migrations;
using api.Models;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ActorServices : IActorServices
    {

        private readonly IActorRepository _repo; 
        public ActorServices(IActorRepository repo)
        {
            _repo = repo; 
        }
        public async Task<ActorResponseDto> CreateAsync(CreateActorDto dto)
        {
           Actor actor = new Actor(); 
           actor.Name = dto.Name; 
           actor.Country = dto.Country; 
           actor.BirthYear = dto.BirthYear; 

           Actor saved = await _repo.CreateAsync(actor); 
           return ToDto(saved); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Actor? actor = await _repo.GetByIdAsync(id); 
            if(actor is null)
            {
                return false; 
            }
           await _repo.DeleteAsync(actor); 
            return true; 
        }

        public async Task<List<ActorResponseDto>> GetAllAsync()
        {
            List<Actor> actor = await _repo.GetAllAsync(); 
             return actor.Select(ToDto).ToList();
        }

        public async Task<ActorResponseDto?> GetByIdAsync(int id)
        {
            Actor? actor = await _repo.GetByIdAsync(id); 
            if(actor is null)
            {
                return null; 
            }
            return ToDto(actor); 
        }

        public async Task<ActorResponseDto> UpdateAsync(int id, UpdateActorDto dto)
        {
           Actor? actor = await _repo.GetByIdAsync(id); 
            if(actor is null)
            {
                return null; 
            }
          actor.Name = dto.Name; 
          actor.Country = dto.Country; 
          actor.BirthYear = dto.BirthYear; 

          await _repo.UpdateAsync(actor); 
          return ToDto(actor); 
        }

        private static ActorResponseDto ToDto(Actor actor)
        {
            ActorResponseDto dto = new ActorResponseDto();  
            dto.Id = actor.Id; 
            dto.Name = actor.Name; 
            dto.Country = actor.Country; 
            dto.BirthYear = actor.BirthYear; 

            return dto; 
        }

        public async Task<bool> ExistsAsync(CreateActorDto dto)
        {
             List<Actor> actor = await _repo.GetAllAsync(); 
             bool exists = actor .Any(w => w.Name.ToLower() == dto.Name.ToLower());
            if (exists)
            {
                return true; 
            }
            return false; 
        }
         public async Task<bool> UpdateExistsAsync(UpdateActorDto dto)
        {
             List<Actor> actor = await _repo.GetAllAsync(); 
            bool exists = actor.Any(w => w.Name.ToLower() == dto.Name.ToLower());
            
            
            if (exists)
            {
                return true; 
            }
            return false; 
        }
    }
}