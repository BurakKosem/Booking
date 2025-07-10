using Bus.Application.DTOs;
using Bus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSharedServices(typeof(CreateBusTripDto).Assembly);
builder.Services.AddDbContext<BusDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("BusDbConnection"));
});

var app = builder.Build();

app.UseSharedMiddleware();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BusDbContext>();
    await DataSeed.SeedAsync(db);
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();