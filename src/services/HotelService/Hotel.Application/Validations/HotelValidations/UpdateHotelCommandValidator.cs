using System;
using FluentValidation;
using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Domain.Enums;

namespace HotelService.Hotel.Application.Validations.HotelValidations;

public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelDto>
{
    public UpdateHotelCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Hotel ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Hotel name is required")
            .Length(2, 100)
            .WithMessage("Hotel name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Z0-9\s\-&'.]+$")
            .WithMessage("Hotel name contains invalid characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Stars)
            .InclusiveBetween(1, 5)
            .WithMessage("Hotel stars must be between 1 and 5");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^[\+]?[1-9][\d]{0,15}$")
            .WithMessage("Invalid phone number format")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street address is required")
            .Length(5, 200)
            .WithMessage("Street address must be between 5 and 200 characters");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required")
            .Length(2, 50)
            .WithMessage("City must be between 2 and 50 characters")
            .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")
            .WithMessage("City contains invalid characters");

        RuleFor(x => x.District)
            .NotEmpty()
            .WithMessage("District is required")
            .Length(2, 50)
            .WithMessage("District must be between 2 and 50 characters")
            .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")
            .WithMessage("District contains invalid characters");

        RuleFor(x => x.HotelType)
            .NotEmpty()
            .WithMessage("Hotel type is required")
            .Must(BeValidHotelType)
            .WithMessage("Invalid hotel type. Valid types: Boutique, Luxury, Resort");

        RuleFor(x => x.CheckInTime)
            .Must(BeValidCheckInTime)
            .WithMessage("Check-in time must be between 06:00 and 18:00")
            .When(x => x.CheckInTime.HasValue);

        RuleFor(x => x.CheckOutTime)
            .Must(BeValidCheckOutTime)
            .WithMessage("Check-out time must be between 06:00 and 15:00")
            .When(x => x.CheckOutTime.HasValue);

        RuleFor(x => x)
            .Must(HaveValidCheckInOutTimes)
            .WithMessage("Check-out time must be before check-in time")
            .When(x => x.CheckInTime.HasValue && x.CheckOutTime.HasValue);

        RuleFor(x => x.Images)
            .Must(HaveValidImageCount)
            .WithMessage("Hotel must have between 1 and 10 images");

        RuleForEach(x => x.Images)
            .Must(BeValidImageUrl)
            .WithMessage("Invalid image URL format");

        RuleFor(x => x)
            .Must(HaveAtLeastOneFacility)
            .WithMessage("Hotel must have at least one facility");

        RuleFor(x => x)
            .Must(HaveValidUpdateData)
            .WithMessage("At least one field must be provided for update");
    }

    private bool BeValidHotelType(string hotelType)
    {
        return Enum.TryParse<HotelType>(hotelType, true, out _);
    }

    private bool BeValidCheckInTime(TimeOnly? checkInTime)
    {
        if (!checkInTime.HasValue) return true;
        var time = checkInTime.Value;
        return time >= new TimeOnly(6, 0) && time <= new TimeOnly(18, 0);
    }

    private bool BeValidCheckOutTime(TimeOnly? checkOutTime)
    {
        if (!checkOutTime.HasValue) return true;
        var time = checkOutTime.Value;
        return time >= new TimeOnly(6, 0) && time <= new TimeOnly(15, 0);
    }

    private bool HaveValidCheckInOutTimes(UpdateHotelDto dto)
    {
        if (!dto.CheckInTime.HasValue || !dto.CheckOutTime.HasValue)
            return true;

        return dto.CheckOutTime.Value < dto.CheckInTime.Value;
    }

    private bool HaveValidImageCount(List<string> images)
    {
        return images != null && images.Count >= 1 && images.Count <= 10;
    }

    private bool BeValidImageUrl(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return false;

        return Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) &&
               (imageUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                imageUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                imageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                imageUrl.EndsWith(".webp", StringComparison.OrdinalIgnoreCase));
    }

    private bool HaveAtLeastOneFacility(UpdateHotelDto dto)
    {
        return dto.HasPool || dto.HasGym || dto.HasSpa ||
               dto.HasRestaurant || dto.HasBar ||
               dto.HasConferenceRoom || dto.PetFriendly;
    }

    private bool HaveValidUpdateData(UpdateHotelDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Name) ||
               !string.IsNullOrWhiteSpace(dto.Description) ||
               dto.Stars > 0 ||
               !string.IsNullOrWhiteSpace(dto.PhoneNumber) ||
               !string.IsNullOrWhiteSpace(dto.Street) ||
               !string.IsNullOrWhiteSpace(dto.City) ||
               !string.IsNullOrWhiteSpace(dto.District) ||
               !string.IsNullOrWhiteSpace(dto.HotelType) ||
               dto.CheckInTime.HasValue ||
               dto.CheckOutTime.HasValue ||
               (dto.Images != null && dto.Images.Any());
    }
}
