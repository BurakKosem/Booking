using Bus.Application.DTOs;
using Bus.Application.Features.CreateBusTrip;
using Bus.Application.Features.SearchBusTrips;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Result;

namespace Bus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<ActionResult<Result>> SearchBusTrips([FromQuery] SearchBusTripsDto searchBusTripsDto)
        {
            var result = await _mediator.Send(new SearchBusTripsQuery(searchBusTripsDto));

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateBusTrip([FromBody] CreateBusTripDto command)
        {
            var result = await _mediator.Send(new CreateBusTripCommand(command));

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
