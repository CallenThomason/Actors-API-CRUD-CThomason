
using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

//Our Controller Layer: Takes in HTTP Requests and Sends back HTTP Response. 
//It only talks to our Services, Never to our DB. 

namespace api.Controllers
{
    [ApiController] //This line turns on our API features. ie. Automatic 400 status code
    [Route("api/[controller]")] //Makes every url endpoint start with api/movie
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _services; 

        public MovieController(IMovieServices services)
        {
            _services = services; 
        }

        //async Task<...>: The action awaits the service, so it returns a Task
        //ASP.NET Core awaits the Task for us, then sends the response. 

        [HttpGet]
        public async Task<ActionResult<List<MovieResponseDto>>> GetAll()
        {
            //await frees this thread while the database works
            //Allows the server to handle other requests
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet("getById/{id}")]

        //Get api/movie/getbyid/5
        //return 200 with the movie, or 400 if no movie is in the db that matches
        public async Task<ActionResult<MovieResponseDto>> GetById(int id)
        {
            MovieResponseDto? movie = await _services.GetByIdAsync(id); 
            if(movie is null)
            {
                return NotFound($"No Movie with {id} exists"); //sends a 404 response
            }
                return Ok(movie); 
        }
        //Post api/movie/addmovie
        //JSON body becomes our CreateMovieDto
        //return 201 created with the new movie, plus our location headers
        [HttpPost("AddMovie")]
        public async Task<ActionResult<MovieResponseDto>> AddMovie([FromBody] CreateMovieDto dto)
        {
            MovieResponseDto movie = await _services.CreateAsync(dto);


            //Created at action sends a 201, It uses getbyid action and the new Id
            //To build the location headers
            return CreatedAtAction(nameof(GetById), new {id = movie.Id}, movie ); 
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<MovieResponseDto>> Update(int id, [FromBody] UpdateMovieDto dto)
        {
            MovieResponseDto? movie = await _services.UpdateAsync(id, dto); 
            if(movie is null)
            {
                return NotFound(); 
            }
            return Ok(movie); 
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           bool isDeleted = await _services.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }
    }
}