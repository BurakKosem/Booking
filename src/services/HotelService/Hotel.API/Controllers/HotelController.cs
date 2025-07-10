using Hotel.Application.DTOs.HotelDTOs;
using Hotel.Application.Features.HotelFeatures.Queries.SearchHotel;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Application.Features.HotelFeatures.Comands.CreateHotel;
using HotelService.Hotel.Application.Features.HotelFeatures.Comands.UpdateHotel;
using HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetAllHotels;
using HotelService.Hotel.Application.Features.HotelFeatures.Queries.GetHotelById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.Result;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Result>> GetAllHotels()
        {
            var hotels = await _mediator.Send(new GetAllHotelsQuery());
            if (hotels.IsSuccess)
                return Ok(hotels);
            return BadRequest(hotels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetHotelById(Guid id)
        {
            var hotel = await _mediator.Send(new GetHotelByIdQuery(id));
            if (hotel.IsSuccess)
                return Ok(hotel);
            return NotFound(hotel);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateHotel([FromBody] CreateHotelDto createHotelDto)
        {
            var result = await _mediator.Send(new CreateHotelCommand(createHotelDto));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> UpdateHotel(Guid id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
                return BadRequest("Id in route and body must match.");

            var result = await _mediator.Send(new UpdateHotelCommand(updateHotelDto));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Result>> SearchHotels([FromQuery] SearchHotelDto searchHotelDto)
        {
            var result = await _mediator.Send(new SearchHotelQuery(searchHotelDto));

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
