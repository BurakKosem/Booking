using System;
using AutoMapper;
using Bus.Domain.Entities;
using Bus.Infrastructure.Data;
using MediatR;
using SharedService.Result;

namespace Bus.Application.Features.CreateBusTrip;

public class CreateBusTripCommandHandler(BusDbContext _dbContext, IMapper _mapper) : IRequestHandler<CreateBusTripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateBusTripCommand request, CancellationToken cancellationToken)
    {
        var mappedBusTrip = _mapper.Map<BusTrip>(request.CreateBusTripDto);
        await _dbContext.BusTrips.AddAsync(mappedBusTrip);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            return Result<Guid>.Failure("Bus trip creation failed.");
        }

        return Result<Guid>.Success(mappedBusTrip.Id);

    }
}
