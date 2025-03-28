using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

app.MapCarter();

app.Run();