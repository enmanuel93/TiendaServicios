using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using StoreService.Api.Gateway.ImplementRemote;
using StoreService.Api.Gateway.InterfaceRemote;
using StoreService.Api.Gateway.MessageHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add ocelot service
// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IAutorRemote, AutorRemote>();

builder.Services.AddHttpClient("AutorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});
builder.Services.AddSingleton<IAutorRemote, AutorRemote>();


builder.Services.AddOcelot().AddDelegatingHandler<LibroHandler>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

//add ocelot middleware
app.UseOcelot().Wait();

app.MapControllers();

app.Run();
