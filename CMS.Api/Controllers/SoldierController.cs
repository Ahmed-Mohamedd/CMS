using CMS.Application.Features.Soldiers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SoldierController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetSoldierByMilitartServiceEndDate(DateTime militaryServiceEndDate)
        {
            var command = new GetSoldierByMilitaryServiceEndDateQuery(militaryServiceEndDate);
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
