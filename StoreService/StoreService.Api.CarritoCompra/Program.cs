using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.CarritoCompra.Application;
using StoreService.Api.CarritoCompra.Persistence;
using StoreService.Api.CarritoCompra.RemoteInterface;
using StoreService.Api.CarritoCompra.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CarritoContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("ConexionDatabase"));
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

builder.Services.AddScoped<ILibroService, LibrosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
