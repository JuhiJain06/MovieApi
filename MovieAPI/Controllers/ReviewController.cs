
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/movies/{movieId}/reviews")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewServices;

        public ReviewController(IReviewService service)
        {
            _reviewServices = service;
        }

        //get all the review

        [HttpGet()]
        public async Task<IActionResult> GetAll(int movieId)
        {
            var reviews = await _reviewServices.Get(movieId);
            return Ok(reviews);
        }

        //get producers by giving id as an input

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int movieId,int id)
        {
            var review =await _reviewServices.Get(movieId,id);
            return Ok(review);
        }

        //add a review
        [HttpPost()]
        public async Task<IActionResult> Add(int movieId ,[FromBody]ReviewRequestModel request)
        {
            var id = await _reviewServices.Add(movieId,request);
            return CreatedAtAction(nameof(GetById), new { movieId,id },id);
        }

        //Delete a review

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int movieId,int id)
        {
            await _reviewServices.Delete(movieId,id);
            return NoContent();
        }

        //update all fields 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int movieId,int id, [FromBody]ReviewUpdateRequest request)
        {
            await _reviewServices.Update(movieId,id, request);
            return Ok();
        }

    }
}
