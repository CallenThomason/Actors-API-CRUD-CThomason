

using api.Models;

namespace api.Repositories
{
    public interface IActorRepository
    {
        List<Actor> GetAll(); 
        Actor? GetById(int id);
        Actor Create(Actor actor);
        void Update(Actor actor); 
        void Delete(Actor actor); 
    }
}