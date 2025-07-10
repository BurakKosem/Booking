using System;
using AutoMapper;
using HotelService.Hotel.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedService.Result;

namespace HotelService.Hotel.Application.Features.HotelFeatures.Comands.UpdateHotel;

public class UpdateHotelCommandHandler(HotelDbContext _dbContext, IMapper _mapper) : IRequestHandler<UpdateHotelCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var existingHotel = await _dbContext.Hotels
            .Where(x => x.Id == request.UpdateHotelDto.Id)
            .Include(x => x.Rooms)
            .Include(x => x.Reviews)
            .ToListAsync(cancellationToken);

        if (existingHotel is null)
        {
            return Result<bool>.Failure("Hotel not found.");
        }
        _mapper.Map(request.UpdateHotelDto, existingHotel);
        _dbContext.Entry(existingHotel).State = EntityState.Modified;
        
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result > 0)
        {
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure("Failed to update hotel.");
    }
}
