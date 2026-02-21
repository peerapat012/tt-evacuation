using Scalar.AspNetCore;
using StackExchange.Redis;
using tt_api.Datas;
using tt_api.Services;
using tt_api.Services.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")
    ));
builder.Services.AddOpenApi();
builder.Services.AddSingleton<EvacuationStateService>();
builder.Services.AddControllers();

builder.Services.AddScoped<IEvacuationService, EvacuationService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// app.MapGet("/api/evacuation/status", () => EvacuationZoneMockupData.EvacuationZones);
// app.MapGet("/api/vehicals", () => VehicalMockupData.Vehicals);
app.MapControllers();

app.Run();