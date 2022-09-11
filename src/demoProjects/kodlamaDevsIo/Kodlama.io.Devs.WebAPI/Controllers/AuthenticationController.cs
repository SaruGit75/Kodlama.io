using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Authentication.Commands.Login;
using Kodlama.io.Devs.Application.Features.Authentication.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            AccessToken token = await Mediator.Send(command);
            return Ok(token);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            AccessToken token = await Mediator.Send(command);

            return Ok(token);
        }
    }
}
