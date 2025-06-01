
using System.Threading;
using CMS.Application.Features.Persons.Commands.CreatePerson;
using CMS.Application.Features.Persons.Commands.UpdatePerson;
using CMS.Application.Features.Persons.DTOs;
using CMS.Application.Features.Persons.Queries.GetPersonById;
using CMS.Application.Features.Persons.Queries.GetPersonByMilitaryNumber;
using CMS.Application.Features.Persons.Queries.GetPersonByNationalId;
using CMS.Application.Features.Persons.Queries.GetPersons;
using CMS.Application.Features.Persons.Queries.SearchingPerson;
using CMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILeaveService _leaveService;

        public PersonsController(IMediator mediator , ILeaveService leaveService)
        {
            _mediator = mediator;
            _leaveService = leaveService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonDto dto)
        {
            var command = new CreatePersonCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
            //return CreatedAtAction(nameof(GetById), new { Id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] UpdatePersonDto dto)
        {
            var command = new UpdatePersonCommand(id, dto);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var query = new GetPersonByIdQuery(Id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("NationalId")]
        public async Task<IActionResult> GetByNationalId(string NationalId)
        {
            var query = new GetPersonByNationalIdQuery(NationalId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("MilitaryNumber")]
        public async Task<IActionResult> GetByMilitaryNumber(string MilitaryNumber)
        {
            var query = new GetPersonByMilitaryNumberQuery(MilitaryNumber);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> search([FromQuery]string SearchQuery)
        {
            var query = new SearchingPersonQuery(SearchQuery);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> search([FromQuery] GetPersonsQuery getPersonsQuery)
        {
            var result = await _mediator.Send(getPersonsQuery);
            return Ok(result);
        }
        [HttpPut("UpdateAbsence")]
        public async Task<IActionResult> UpdatePersonAbsence()
        {
            // This method is used to update the absence status of persons based on their latest leave records.
            await _leaveService.UpdateLeaveStatusAsync(CancellationToken.None);
            return Ok();
        }


    }
}
