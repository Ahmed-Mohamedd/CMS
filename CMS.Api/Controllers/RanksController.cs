using CMS.Application.Features.Branches.Commands.UpdateBranch;
using CMS.Application.Features.Ranks.Commands.CreateRank;
using CMS.Application.Features.Ranks.Commands.DeleteRank;
using CMS.Application.Features.Ranks.Commands.UpdateRank;
using CMS.Application.Features.Ranks.DTOs;
using CMS.Application.Features.Ranks.Queries.GetRankById;
using CMS.Application.Features.Ranks.Queries.GetRanks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RanksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RanksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRankDto dto)
        {
            var command = new CreateRankCommand(dto);
            var result = await _mediator.Send(command);

            return Ok(result.id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateRankDto dto)
        {
            var command = new UpdateRankCommand(id, dto);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteRankCommand(id);
            var result = await _mediator.Send(command);
            if (result == true)
                return Ok(result);
            return BadRequest($"Can't delete rank with id {id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRankById(int id)
        {
            var command = new GetRankByIdQuery(id);
            var result = await _mediator.Send(command);
            if (result != null)
                return Ok(result);
            return BadRequest($"Can't Get Rank with Id {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetRanks([FromQuery] GetRanksQuery getRanksQuery)
        {
            var result = await _mediator.Send(getRanksQuery);
            return Ok(result);
        }
    }
}
