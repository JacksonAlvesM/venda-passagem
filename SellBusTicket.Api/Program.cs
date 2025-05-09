using Microsoft.EntityFrameworkCore;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Infrastructure.Data;
using SellBusTicket.Infrastructure.Repositories.InMemory;
using SellBusTicket.Infrastructure.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TicketSaleDb"));

builder.Services.AddSingleton<IPlaceRepository, InMemoryPlaceRepository>();
builder.Services.AddSingleton<IRouteRepository, InMemoryRouteRepository>();
builder.Services.AddSingleton<ISeatRepository, InMemorySeatRepository>();
builder.Services.AddSingleton<ITripRepository, InMemoryTripRepository>();

builder.Services.AddTransient<DataSeeder>();


builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellBusTicket API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed inicial
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedData();
}

app.Run();