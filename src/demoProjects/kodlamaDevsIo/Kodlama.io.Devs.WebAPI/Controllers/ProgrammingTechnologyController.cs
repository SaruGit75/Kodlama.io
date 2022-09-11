using System.Net;
using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.HardDeleteProgrammingTechnologies;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.SoftDeleteProgrammingTechnologies;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnologies;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Queries.GetByIdProgrammingTechnology;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingTechnologyController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingTechnologyQuery query = new()
            {
                PageRequest = pageRequest
            };

            ProgrammingTechnologyListModel model = await Mediator.Send(query);
            return Ok(model);
        }

        //TODO getbyId yapılacak.   Mappingprofile eklemek gerek
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            GetByIdProgrammingTechnologyQuery query = new()
            {
                Id = Id
            };

            ProgrammingTechnologyGetByIdDto technologyGetByIdDto = await Mediator.Send(query);
            return Ok(technologyGetByIdDto);
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingTechnologyCommand createProgrammingTechnologyCommand)
        {
            var createProgrammingTech = await Mediator.Send(createProgrammingTechnologyCommand);
            return Created("", createProgrammingTech);
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> SoftDelete([FromRoute] int Id)
        {
            SoftDeleteProgrammingTechnologyCommand softDeletedEntity = new()
            {
                Id = Id
            };

            SoftDeleteProgrammingTechnologyDto languageTechnologyDto = await Mediator.Send(softDeletedEntity);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> HardDelete([FromRoute] int Id)
        {
            HardDeleteProgrammingTechnologyCommand softDeletedEntity = new()
            {
                Id = Id
            };

            HardDeleteProgrammingTechnologyDto languageTechnologyDto = await Mediator.Send(softDeletedEntity);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingTechnologyCommand request)
        {
            UpdatedProgrammingTechnologyDto programmingTechnologyDto =
                await Mediator.Send(request);

            return Ok(programmingTechnologyDto);
        }

    }
}