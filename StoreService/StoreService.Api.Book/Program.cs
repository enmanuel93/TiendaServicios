using Microsoft.EntityFrameworkCore;
using StoreService.Api.Book.Persistence;
using MediatR;
using StoreService.Api.Book.Application;
using FluentValidation.AspNetCore;
using StoreService.RabbitMQ.Bus.BusRabbit;
using StoreService.RabbitMQ.Bus.Implement;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();

builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
});

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

builder.Services.AddDbContext<LibreriaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDb"));
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
