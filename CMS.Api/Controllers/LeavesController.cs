using CMS.Application.Features.Leaves.Commands.AddAnnualLeave;
using CMS.Application.Features.Leaves.Commands.AddCasualLeave;
using CMS.Application.Features.Leaves.Commands.AddLeave;
using CMS.Application.Features.Leaves.Commands.DeleteLeave;
using CMS.Application.Features.Leaves.Commands.UpdateLeave;
using CMS.Application.Features.Leaves.DTOs;
using CMS.Application.Features.Leaves.Queries.GetLeaves;
using CMS.Application.Features.Persons.Queries.GetPersons;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeavesController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<IActionResult> GetLeaves([FromQuery] GetLeavesQuery getLeavesQuery)
        {
            var result = await _mediator.Send(getLeavesQuery);
            return Ok(result);
        }




        [HttpPost]
        public async Task<IActionResult> CreateLeave([FromBody] AddLeaveDto dto)
        {
            var command = new AddAllTypesOfLeaveExceptAnnualandCasualCommand(dto);
            var result = await _mediator.Send(command);
            if(result == 0)
                return BadRequest("Failed to create leave request. As The person Not Eligable For this leave");

            return Ok(result);
        }

        [HttpPost("AddCasualLeaveForOff")]
        public async Task<IActionResult> AddCasualLeave([FromBody] AddCasualLeaveDto dto)
        {
            var command = new AddCasualLeaveCommand(dto);
            var result = await _mediator.Send(command);
            if (result == 0)
                return BadRequest("Failed to add casual leave for this person");

            return Ok(result);
        }

        [HttpPost("AddAnnualLeaveForOff")]
        public async Task<IActionResult> AddAnnualLeave([FromBody] AddAnnualLeaveDto dto)
        {
            var command = new AddAnnualLeaveCommand(dto);
            var result = await _mediator.Send(command);
            if (result == 0)
                return BadRequest("Failed to add annual leave for this person");

            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateLeave( int Id ,[FromBody] UpdateLeaveDto dto)
        {
            var command = new UpdateLeaveCommand(Id,dto);
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Failed to update leave request.");

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteLeave(int Id)
        {
            var command = new DeleteLeaveCommand(Id);
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Failed to delete leave request.");
            return Ok(result);
        }
    }
}
