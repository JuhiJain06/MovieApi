
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [AllowAnonymous]
    public class MovieController : ControllerBase
    {
       private readonly IMovieService _movieServices;

        public MovieController(IMovieService service,IMovieService service1)
        {
            _movieServices = service;
        }
        //get all the movies from the movies

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieServices.Get();
            return Ok(movies);
        }

        //get movies by giving id as an input

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _movieServices.Get(id);
            return Ok(movie);
        }

        //add a movie
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]MovieRequestModel request)
        {
            var id = await _movieServices.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        //Delete a movie

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieServices.Delete(id);
            return NoContent();
        }


        //update all fields 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MovieUpdateRequest request)
        {
            await _movieServices.Update(id, request);
            return Ok();
        }

    }
}
