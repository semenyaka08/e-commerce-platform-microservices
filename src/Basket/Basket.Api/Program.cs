using Basket.Api.Data;
using Basket.Api.Middlewares;
using Basket.Api.Models;
using BuildingBlocks.Behaviors;
using Carter;
using Discount.Grpc;
using FluentValidation;
using HealthChecks.UI.Client;
using Marten;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), host =>
        {
            host.Username(builder.Configuration["MessageBroker:UserName"]);
            host.Password(builder.Configuration["MessageBroker:Password"]);
        });
        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, BasketCashingRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

var app = builder.Build();

app.MapCarter();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();