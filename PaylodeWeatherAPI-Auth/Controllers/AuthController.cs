using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaylodeWeatherAPI.Contracts;
using PaylodeWeatherAPI.Credential_Auth.Models;

namespace PaylodeWeatherAPI_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {

            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            var result = await _authentication.Login(user);
            return Ok(new AuthenticatedResponse { Token = result });
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            if (signUpDto == null)
            {
                return BadRequest("Invalid Input");
            }
            var registration = await _authentication.SignUp(signUpDto);
            return Ok(registration);
        }
    }
}

