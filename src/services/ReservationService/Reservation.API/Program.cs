using Microsoft.EntityFrameworkCore;
using Reservation.Application.DTOs;
using Reservation.Infrastructure.Data;
using SharedService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSharedServices(typeof(CreateReservationDto).Assembly);
builder.Services.AddDbContext<ReservationDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("ReservationDbConnection"));
});

var app = builder.Build();

app.UseSharedMiddleware();
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<BusDbContext>();
//     await DataSeed.SeedAsync(db);
// }
app.UseHttpsRedirection();
app.MapControllers();


app.Run();