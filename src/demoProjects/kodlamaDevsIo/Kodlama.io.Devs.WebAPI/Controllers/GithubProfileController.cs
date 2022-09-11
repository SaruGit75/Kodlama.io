using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.HardDeleteGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetListGithubProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubProfileController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubProfileQuery query = new GetListGithubProfileQuery
            {
                PageRequest = pageRequest
            };
            GithubProfileListModel result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubProfileCommand command)
        {
            CreatedGithubProfileDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] HardDeleteGithubProfileCommand command)
        {
            HardDeletedGithubProfileDto result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubProfileCommand command)
        {
            UpdatedGithubProfileDto result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
