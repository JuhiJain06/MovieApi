

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreServices;

        public GenreController(IGenreService service)
        {
            _genreServices = service;
        }
        //get all the genres

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres=await _genreServices.Get();
            return Ok(genres);
        }

        //get genre by giving id as an input

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreServices.Get(id);
            return Ok(genre);
        }

        //add a genre
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]GenreRequestModel request)
        {
            var id = await _genreServices.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        //Delete a genre

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreServices.Delete(id);
            return NoContent();
        }

        //update all fields 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]GenreUpdateRequest request)
        {
            await _genreServices.Update(id, request);
            return Ok();
        }
    }
}
