using System;
using AutoMapper;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Comands.CreateHotel;

public class CreateHotelCommandHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<CreateHotelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = _mapper.Map<Domain.Entities.Hotel>(request.createHotelDto);

        if (hotel is null)
        {
            return Result<Guid>.Failure("Please provide valid hotel details.");
        }

        await _dbContext.Hotels.AddAsync(hotel, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result > 0)
        {
            return Result<Guid>.Success(hotel.Id);
        }

        return Result<Guid>.Failure("Failed to create hotel.");
    }
}
