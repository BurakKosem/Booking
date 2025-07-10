using System;
using FluentValidation;
using Hotel.Application.DTOs.RoomDTOs;

namespace Hotel.Application.Validations.RoomValidations;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomDto>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Room ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Room ID cannot be an empty GUID.");
            
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Room name is required.")
            .MaximumLength(100).WithMessage("Room name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.TotalRoomCount)
            .GreaterThan(0).WithMessage("Room count must be greater than 0.");

        RuleFor(x => x.MaxOccupancy)
            .GreaterThan(0).WithMessage("Max occupancy must be greater than 0.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Size must be greater than 0.");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greater than 0.");

        RuleFor(x => x.WeekendPriceMultiplier)
            .GreaterThanOrEqualTo(1).WithMessage("Weekend price multiplier must be at least 1.");

        RuleFor(x => x.SeasonPriceMultiplier)
            .GreaterThanOrEqualTo(1).WithMessage("Season price multiplier must be at least 1.");
    }
}
