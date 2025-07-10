using MediatR;
using Plane.Applicaton.DTOs;
using SharedService.Result;

namespace Plane.Applicaton.Features.SearchFlights;

public record SearchFlightsQuery(SearchFlightsDto searchFlightsDto) : IRequest<Result<PagedResult<FlightsDto>>>;
