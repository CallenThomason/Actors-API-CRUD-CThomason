using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorServices _services; 
        public ActorController(IActorServices services)
        {
            _services = services; 
        }
        [HttpGet]
        public ActionResult<List<ActorResponseDto>> GetAll()
        {
            return Ok(_services.GetAll()); 
        }

        [HttpGet("{id}")]
        public ActionResult<ActorResponseDto> GetById(int id)
        {
            ActorResponseDto? found = _services.GetById(id); 
            if(found is null)
            {
                return NotFound($"Actor with the id {id} was not found"); 
            }
            return Ok(found); 
        }
        [HttpPost("create")]
        public ActionResult<ActorResponseDto> Create([FromBody] CreateActorDto dto)
        {
            ActorResponseDto actor = _services.Create(dto);
            if (_services.Exists(dto))
            {
                return Conflict($"An Actor with the name {dto.Name} alrweady exists.");
            }
             return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor ); 
        }

        [HttpPut("update/{id}")]
        public ActionResult<ActorResponseDto> Update(int id, [FromBody] UpdateActorDto dto)
        {
            ActorResponseDto? actor = _services.Update(id, dto);
            if(actor is null)
            {
                return NotFound($"Actor with id {id} could not be found");
            }
            if (_services.UpdateExists(dto))
            {
                return Conflict($"An Actor with the name {dto.Name} alrweady exists.");
            }
            return Ok(actor); 
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<ActorResponseDto> Delete(int id)
        {
              bool deleted = _services.Delete(id);
            if(!deleted)
            {
                return NotFound($"Actor with id {id} could not be found");
            }
            return NoContent();
        }
    }
}