using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Models;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            GetUserOperationClaimQuery query = new()
            {
                PageRequest = pageRequest
            };
            GetUserOperationClaimListModel result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimsCommand userOperationClaimsCommand)
        {
            CreateUserOperationClaimsDto userOperationClaimsDto = await Mediator.Send(userOperationClaimsCommand);
            return Created("", userOperationClaimsDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateItem([FromBody]UpdateUserOperationClaimsCommand updateUserOperationClaimsCommand)
        {
            UpdatedUserOperationClaimDto result = await Mediator.Send(updateUserOperationClaimsCommand);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimsCommand)
        {
            DeleteUserOperationClaimDto result = await Mediator.Send(deleteUserOperationClaimsCommand);
            return Ok(result);
        }
    }
}
