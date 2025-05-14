using CMS.Application.Features.Branches.Commands.CreateBranch;
using CMS.Application.Features.Branches.Commands.DeleteBranch;
using CMS.Application.Features.Branches.Commands.UpdateBranch;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.Branches.Queries.GetBranchById;
using CMS.Application.Features.Branches.Queries.GetBranchs;
using CMS.Application.Features.Branches.Queries.GetPersonsWithSpecificBranch;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto dto)
        {
            var command = new CreateBranchCommand(dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBranchDto dto)
        {
            var command = new UpdateBranchCommand(id, dto);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBranchCommand(id);
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok(result);
            return BadRequest($"Can't delete branch with id {id}"); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var command = new GetBranchByIdQuery(id);
            var result = await _mediator.Send(command);
            if (result != null)
                return Ok(result);
            return BadRequest($"Can't Get Bransh with Id {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetBranchs([FromQuery] GetBranchsQuery getBranchQuery)
        {
            var result = await _mediator.Send(getBranchQuery);
            return Ok(result);
        }

        [HttpGet("GetPersonsWithBranchId/{id}")]
        public async Task<IActionResult> GetPersonsWithBranchId(int id)
        {
            var command = new GetPersonsWithSpecificBranchIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
