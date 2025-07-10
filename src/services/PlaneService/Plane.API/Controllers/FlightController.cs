using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plane.Applicaton.DTOs;
using Plane.Applicaton.Features.CreateFlight;
using Plane.Applicaton.Features.SearchFlights;
using SharedService.Result;

namespace Plane.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<ActionResult<Result>> SearchFlights([FromQuery] SearchFlightsDto searchFlightsDto)
        {
            var result = await _mediator.Send(new SearchFlightsQuery(searchFlightsDto));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreateFlight([FromBody] CreateFlightDto createFlightDto)
        {
            var result = await _mediator.Send(new CreateFlightCommand(createFlightDto));

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
