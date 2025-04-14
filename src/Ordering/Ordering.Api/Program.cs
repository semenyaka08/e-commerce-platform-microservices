using Ordering.Api.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data.Extensions;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentationServices();

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();