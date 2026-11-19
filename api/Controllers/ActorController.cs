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
        public async Task<ActionResult<List<ActorResponseDto>>> GetAll()
        {
            return Ok( await _services.GetAllAsync()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorResponseDto>> GetById(int id)
        {
            ActorResponseDto? found = await _services.GetByIdAsync(id); 
            if(found is null)
            {
                return NotFound($"Actor with the id {id} was not found"); 
            }
            return Ok(found); 
        }
        [HttpPost("create")]
        public async Task<ActionResult<ActorResponseDto>> Create([FromBody] CreateActorDto dto)
        {
            
            if (await _services.ExistsAsync(dto))
            {
                return Conflict($"An Actor with the name {dto.Name} alrweady exists.");
            }
            else{
                ActorResponseDto actor = await _services.CreateAsync(dto);
             return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor ); 
             }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ActorResponseDto>> Update(int id, [FromBody] UpdateActorDto dto)
        {
           
            bool exiting = await _services.UpdateExistsAsync(dto); 
           
             if (exiting)
            {
                return Conflict($"An Actor with the name {dto.Name} alrweady exists.");
            }
            else{
                 ActorResponseDto? actor = await _services.UpdateAsync(id, dto);
                  if(actor is null)
            {
                return NotFound($"Actor with id {id} could not be found");
            }
            return Ok(actor); 
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ActorResponseDto>> Delete(int id)
        {
              bool deleted = await _services.DeleteAsync(id);
            if(!deleted)
            {
                return NotFound($"Actor with id {id} could not be found");
            }
            return NoContent();
        }
    }
}