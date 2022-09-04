using System.Net;
using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.HardDeleteProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.SoftDeleteProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguageController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new()
            {
                PageRequest = pageRequest
            };
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguage)
        {
            CreatedProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguage);
            return Created("", result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery languageQuery)
        {
            ProgrammingLanguageGetByIdDto getByIdDto = await Mediator.Send(languageQuery);
            return Ok(getByIdDto);
        }

        [HttpPost("UpdateItem/{Id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int Id, [FromBody] string langName)
        {
            UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand = new()
            {
                Id = Id,
                Name = langName
            };
            UpdatedProgrammingLanguageDto programmingLanguageDto = await Mediator.Send(updateProgrammingLanguageCommand);
            return Ok(programmingLanguageDto);
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> SoftDelete([FromRoute] int Id)
        {
            SoftDeleteProgrammingLanguageCommand softDeletedEntity = new()
            {
                Id = Id
            };

            SoftDeleteProgrammingLanguageDto languageDto = await Mediator.Send(softDeletedEntity);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> HardDelete([FromRoute] int Id)
        {
            HardDeleteProgrammingLanguageCommand hardDeleteEntity = new()
            {
                Id = Id
            };
            HardDeleteProgrammingLanguageDto languageDto = await Mediator.Send(hardDeleteEntity);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}