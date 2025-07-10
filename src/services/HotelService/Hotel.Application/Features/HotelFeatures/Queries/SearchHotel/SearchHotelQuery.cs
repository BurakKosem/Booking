using Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using MediatR;
using SharedService.Result;

namespace Hotel.Application.Features.HotelFeatures.Queries.SearchHotel;

public record SearchHotelQuery(SearchHotelDto SearchHotelDto) : IRequest<Result<PagedResult<HotelDto>>>;