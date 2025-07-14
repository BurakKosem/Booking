using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.DTOs;
using Reservation.Application.Features.Commands.CancelReservation;
using Reservation.Application.Features.Commands.CreateReservation;
using Reservation.Application.Features.Queries.GetReservationById;
using Reservation.Application.Features.Queries.GetReservationsByUserId;
using SharedService.Result;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Result>> CreateReservation([FromBody] CreateReservationDto createReservationDto)
        {
            var result = await _mediator.Send(new CreateReservationCommand(createReservationDto));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("cancel")]
        public async Task<ActionResult<Result>> CancelReservation(CancelReservationDto cancelReservationDto)
        {
            var result = await _mediator.Send(new CancelReservationCommand(cancelReservationDto));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Result>> GetReservationsByUserId(Guid userId, string reservationType = "", int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetReservationsByUserIdQuery(userId, reservationType, page, pageSize));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Result>> GetReservationById(Guid reservationId)
        {
            var result = await _mediator.Send(new GetReservationByIdQuery(reservationId));
            if (result.IsSuccess)
                return Ok(result);
            return NotFound(result);
        }
    }
}
