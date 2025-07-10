namespace Hotel.Application.DTOs.HotelDTOs;

public record SearchHotelDto
(
    string? Keyword = null,
    string? Name = null,
    string? City = null,
    string? District = null,
    int? Stars = null,
    string? HotelType = null,
    decimal? MinRoomPrice = null,
    int Page = 1,
    int PageSize = 20
);