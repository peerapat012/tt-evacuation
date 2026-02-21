using Scalar.AspNetCore;
using tt_api.Datas;
using tt_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<EvacuationStateService>();
builder.Services.AddControllers();
builder.Services.AddScoped<IEvacuationService, EvacuationService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

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