using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models.RequestModels;
using MovieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Route("api/authorization")]
    [ApiController]
 
    public class AuthController : ControllerBase
    {
            private readonly IAuthService _userService;

            public AuthController(IAuthService userService)
            {
                _userService = userService;
            }


            [HttpPost("signup")]
            public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
            {
                await _userService.SignUp(signUpRequest);


                return Ok();
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequest signIn)
            {
                var result = await _userService.Login(signIn);

                return Ok(result);
            }
        }
}
