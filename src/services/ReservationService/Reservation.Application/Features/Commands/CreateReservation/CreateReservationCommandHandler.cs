using System;
using MediatR;
using Reservation.Domain.Entities;
using Reservation.Domain.Enums;
using Reservation.Domain.ValueObjects;
using Reservation.Infrastructure.Data;
using SharedService.Result;

namespace Reservation.Application.Features.Commands.CreateReservation;

public class CreateReservationCommandHandler(ReservationDbContext _dbContext) : IRequestHandler<CreateReservationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.createReservationDto;

        var reservationType = dto.ReservationType switch
        {
            "Hotel" => ReservationType.Hotel,
            "Flight" => ReservationType.Plane,
            "Bus" => ReservationType.Bus,
            _ => throw new ArgumentException("Invalid reservation type.")
        };
        var customerInfo = new CustomerInfo(dto.CustomerFirstName, dto.CustomerLastName, dto.CustomerEmail);
        var totalAmount = dto.Items.Sum(item => item.Quantity * item.UnitPrice);

        var reservation = Domain.Entities.Reservation.Create(
            dto.UserId,
            reservationType,
            customerInfo,

            totalAmount,
            dto.Items.First().ServiceDate,
            dto.Items.First().EndDate,
            dto.SpecialRequests
        );

        foreach (var itemDto in dto.Items)
        {


            var itemDetails = ItemDetails.Create(
                $"{reservationType}Service",
                itemDto.AdditionalInfo
            );

            var reservationItem = ReservationItem.Create(
                reservation.Id, 
                itemDto.ServiceId, 
                itemDto.SubServiceId,
                itemDetails, 
                itemDto.UnitPrice, 
                itemDto.Quantity, 
                itemDto.ServiceDate, 
                itemDto.EndDate
            );
            reservation.AddReservationItem(reservationItem);
        }

        await _dbContext.Reservations.AddAsync(reservation);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            return Result<Guid>.Failure("Failed to create reservation.");
        }
        return Result<Guid>.Success(reservation.Id);
    }
}
