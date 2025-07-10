using System;
using AutoMapper;
using MediatR;
using Plane.Domain.Entities;
using Plane.Infrastructure.Data;
using SharedService.Result;

namespace Plane.Applicaton.Features.CreateFlight;

public class CreateFlightCommandHandler(PlaneDbContext _dbContext, IMapper _mapper) : IRequestHandler<CreateFlightCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var mappedFlight = _mapper.Map<Flight>(request.CreateFlightDto);

        await _dbContext.Flights.AddAsync(mappedFlight, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result <= 0)
        {
            return Result<Guid>.Failure("Failed to create flight");
        }

        return Result<Guid>.Success(mappedFlight.Id);
    }
}
