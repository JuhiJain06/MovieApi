
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/actors")]
    [ApiController]
    [Authorize]
    
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService service)
        {
            _actorService = service;
        }

        //get all the actor from the actors

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var actors = await _actorService.Get();
            return Ok(actors);
        }

        //get actors by giving id as an input

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var actor = await _actorService.Get(id);
            return Ok(actor);
        }

        //add a actor
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ActorRequestModel request)
        {
             int id=await _actorService.Add(request);
            return CreatedAtAction(nameof(GetById),new { id = id },id);
        }

        //Delete a actor

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            await _actorService.Delete(id);
            return NoContent();
        }

        //update all fields 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id,[FromBody]ActorUpdateRequest request)
        {
            await _actorService.Update(id, request);
            return Ok();
        }

    }
}
