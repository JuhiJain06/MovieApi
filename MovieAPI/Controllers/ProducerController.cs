
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/producers")]
    [ApiController]
    [Authorize]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producerServices;

        public ProducerController(IProducerService service)
        {
            _producerServices = service;
        }
        //get all the producers from the producer

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var producers =await _producerServices.Get();
            return Ok(producers);
        }

        //get producers by giving id as an input

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producer= await _producerServices.Get(id);
            return Ok(producer);
        }

        //add a producer
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProducerRequestModel request)
        {
            var id =await _producerServices.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        //Delete a producer

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            await _producerServices.Delete(id);
            return NoContent();
        }

        //update all fields 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]ProducerUpdateRequest request)
        {
            await _producerServices.Update(id, request);
            return Ok();
        }
    }
}
