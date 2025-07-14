using Hotel.Application.DTOs.RoomDTOs;
using Hotel.Application.Features.RoomFeatures.Commands.CreateRoomCommand;
using Hotel.Application.Features.RoomFeatures.Queries.GetAllRoomsByHotelId;
using Hotel.Application.Features.RoomFeatures.Queries.GetRoomById;
using Hotel.Application.Features.RoomFeatures.Queries.GetRoomsByRoomType;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Result;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{HotelId}")]
        public async Task<ActionResult<Result>> GetRoomsByHotelId(Guid HotelId)
        {
            var rooms = await _mediator.Send(new GetAllRoomsByHotelIdQuery(HotelId));
            if (rooms.IsSuccess)
                return Ok(rooms);
            return BadRequest(rooms);
        }

        [HttpGet("{HotelId}/{RoomType}")]
        public async Task<ActionResult<Result>> GetRoomsByRoomType(Guid HotelId, string RoomType)
        {
            var rooms = await _mediator.Send(new GetRoomsByRoomTypeQuery(HotelId, RoomType));
            if (rooms.IsSuccess)
                return Ok(rooms);
            return BadRequest(rooms);
        }
        [HttpGet("hotel/{RoomId}")]
        public async Task<ActionResult<Result>> GetRoomById(Guid RoomId)
        {
            var room = await _mediator.Send(new GetRoomByIdQuery(RoomId));
            if (room.IsSuccess)
                return Ok(room);
            return NotFound(room);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateRoom([FromBody] CreateRoomDto createRoomDto)
        {
            var result = await _mediator.Send(new CreateRoomCommand(createRoomDto));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
