using MediatR;
using Plane.Applicaton.DTOs;
using SharedService.Result;

namespace Plane.Applicaton.Features.CreateFlight;

public record CreateFlightCommand(CreateFlightDto CreateFlightDto) : IRequest<Result<Guid>>;
