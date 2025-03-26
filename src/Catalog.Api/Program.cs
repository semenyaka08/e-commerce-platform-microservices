using Carter;
using Catalog.Api.Middlewares;
using Marten;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
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