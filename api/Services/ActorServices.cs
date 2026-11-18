

using api.Dtos;
using api.Migrations;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class ActorServices : IActorServices
    {

        private readonly IActorRepository _repo; 
        public ActorServices(IActorRepository repo)
        {
            _repo = repo; 
        }
        public ActorResponseDto Create(CreateActorDto dto)
        {
           Actor actor = new Actor(); 
           actor.Name = dto.Name; 
           actor.Country = dto.Country; 
           actor.BirthYear = dto.BirthYear; 

           Actor saved = _repo.Create(actor); 
           return ToDto(saved); 
        }

        public bool Delete(int id)
        {
            Actor? actor = _repo.GetById(id); 
            if(actor is null)
            {
                return false; 
            }
            _repo.Delete(actor); 
            return true; 
        }

        public List<ActorResponseDto> GetAll()
        {
            List<Actor> actor = _repo.GetAll(); 
             return actor.Select(ToDto).ToList();
        }

        public ActorResponseDto? GetById(int id)
        {
            Actor? actor = _repo.GetById(id); 
            if(actor is null)
            {
                return null; 
            }
            return ToDto(actor); 
        }

        public ActorResponseDto Update(int id, UpdateActorDto dto)
        {
           Actor? actor = _repo.GetById(id); 
            if(actor is null)
            {
                return null; 
            }
          actor.Name = dto.Name; 
          actor.Country = dto.Country; 
          actor.BirthYear = dto.BirthYear; 

          _repo.Update(actor); 
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

        public bool Exists(CreateActorDto dto)
        {
            bool exists = _repo.GetAll().Any(w => w.Name.ToLower() == dto.Name.ToLower());
            if (exists)
            {
                return true; 
            }
            return false; 
        }
         public bool UpdateExists(UpdateActorDto dto)
        {
            bool exists = _repo.GetAll().Any(w => w.Name.ToLower() == dto.Name.ToLower());
            if (exists)
            {
                return true; 
            }
            return false; 
        }
    }
}