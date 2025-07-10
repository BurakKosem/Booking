using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Plane.Applicaton.DTOs;
using Plane.Infrastructure.Data;
using SharedService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSharedServices(typeof(CreateFlightDto).Assembly);

builder.Services.AddDbContext<PlaneDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PlaneDbConnection"));
});

var app = builder.Build();

app.UseSharedMiddleware();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();