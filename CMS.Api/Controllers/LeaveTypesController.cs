using CMS.Application.Features.Branches.Commands.CreateBranch;
using CMS.Application.Features.Branches.Commands.DeleteBranch;
using CMS.Application.Features.Branches.Commands.UpdateBranch;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.Branches.Queries.GetBranchById;
using CMS.Application.Features.Branches.Queries.GetBranchs;
using CMS.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CMS.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using CMS.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById;
using CMS.Application.Features.LeaveTypes.Queries.GetLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveTypeDto dto)
        {
            var command = new CreateLeaveTypeCommand(dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateLeaveTypeDto dto)
        {
            var command = new UpdateLeaveTypeCommand(id, dto);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand(id);
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok(result);
            return BadRequest($"Can't delete leave type with id {id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveTypeById(int id)
        {
            var command = new GetLeaveTypeByIdQuery(id);
            var result = await _mediator.Send(command);
            if (result != null)
                return Ok(result);
            return BadRequest($"Can't Get Leave Type with Id {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveTypes([FromQuery] GetLeaveTypesQuery getLeaveTypesQuery)
        {
            var result = await _mediator.Send(getLeaveTypesQuery);
            return Ok(result);
        }
    }
}
