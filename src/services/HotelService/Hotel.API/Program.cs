using HotelService.Hotel.Application.DTOs.HotelDTOs;
using HotelService.Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSharedServices(typeof(CreateHotelDto).Assembly);
builder.Services.AddDbContext<HotelDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("HotelDbConnection"));
});

var app = builder.Build();

await HotelSeeder.InitializeAsync(app.Services);

app.UseSharedMiddleware();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();