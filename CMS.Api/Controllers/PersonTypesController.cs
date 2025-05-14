using CMS.Application.Features.Persons.Queries.GetPersons;
using CMS.Application.Features.PersonTypes.Commands.CreatePersonType;
using CMS.Application.Features.PersonTypes.DTOs;
using CMS.Application.Features.PersonTypes.Queries.GetPersonTypes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonTypeDto dto)
        {
            var command = new CreatePersonTypeCommand(dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonTypes([FromQuery] GetPersonTypesQuery getPersonTypesQuery)
        {
            var result = await _mediator.Send(getPersonTypesQuery);

            return Ok(result);
        }
    }
}
