using Bus.Application.DTOs;
using MediatR;
using SharedService.Result;

namespace Bus.Application.Features.SearchBusTrips;

public record SearchBusTripsQuery(SearchBusTripsDto searchBusTripsDto) : IRequest<Result<PagedResult<BusTripDto>>>;
