using System.Net;
using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Kodlama.io.Devs.Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Kodlama.io.Devs.Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Models;
using Kodlama.io.Devs.Application.Features.OperationClaims.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            GetOperationClaimListQuery getOperationClaimListQuery = new()
            {
                PageRequest = pageRequest
            };
            OperationClaimListModel listModel = await Mediator.Send(getOperationClaimListQuery);
            return Ok(listModel);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimDto createToOperationClaim = await Mediator.Send(createOperationClaimCommand);
            return Created("", createToOperationClaim);
        }


        [HttpPost("update/{Id}")]
        public async Task<IActionResult> UpdateItem(int Id, [FromBody] string name)
        {
            UpdateOperationClaimCommand updateOperationClaimCommand = new()
            {
                Id = Id,
                Name = name
            };

            UpdateOperationClaimDto updateToOperationClaimDto = await Mediator.Send(updateOperationClaimCommand);
            return Ok(updateToOperationClaimDto);
        }

        [HttpPost("delete/{Id}")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            DeleteOperationClaimCommand deleteToperationClaimCommand = new()
            {
                Id = Id
            };
            DeleteOperationClaimDto deleteOperationClaimDto = await Mediator.Send(deleteToperationClaimCommand);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
