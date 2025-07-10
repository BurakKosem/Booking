using Bus.Application.DTOs;
using MediatR;
using SharedService.Result;

namespace Bus.Application.Features.CreateBusTrip;

public record CreateBusTripCommand(CreateBusTripDto CreateBusTripDto) : IRequest<Result<Guid>>;
