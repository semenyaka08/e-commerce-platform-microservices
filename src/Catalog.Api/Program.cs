using System.Reflection;
using BuildingBlocks.Behaviors;
using Carter;
using Catalog.Api.Middlewares;
using FluentValidation;
using Marten;

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

builder.Services.AddMarten(configuration =>
{
    configuration.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!); 
}).UseLightweightSessions();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

//Configure the HTTP request pipeline
app.MapCarter();

app.Run();