using LiftControlSystem.Domain.Enums;
using LiftControlSystem.Domain.Logic.Managers;
using LiftControlSystem.Controllers;
using Scalar.AspNetCore;
using LiftControlSystem.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<LiftController>();
builder.Services.AddSingleton<SystemModeManager>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapScalarApiReference(o => o.WithTheme(ScalarTheme.Alternate));

app.MapPost("/assignlift/{requestedFloor}", async (int requestedFloor, LiftController controller) =>
{
    var liftResult = await controller.AssignLiftAsync(requestedFloor);
    return liftResult.Success && liftResult.Value is not null
        ? Results.Ok(liftResult.Value.ToDto())
        : Results.NotFound(liftResult.Warnings.Union(liftResult.Errors));
}).WithName("AssignLiftAsync");

app.MapPost("/mode/{mode}", (string mode, SystemModeManager modeManager) =>
{
    if (!Enum.TryParse<LiftStrategies>(mode, ignoreCase: true, out var liftStrategy))
        return Results.BadRequest($"Mode {mode} is not supported.");

    if (modeManager.TrySetMode(liftStrategy))
        return Results.Ok($"Mode changed to {mode}");

    return Results.BadRequest("Invalid mode");
}).WithName("AssignLiftMode");

app.Run();
