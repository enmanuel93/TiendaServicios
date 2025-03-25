using Microsoft.EntityFrameworkCore;
using MediatR;
using StoreService.Api.Author.Persistence;
using StoreService.Api.Author.Application;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using StoreService.RabbitMQ.Bus.BusRabbit;
using StoreService.RabbitMQ.Bus.EventoQueue;
using StoreService.Api.Author.ManejadorRabbit;
using StoreService.RabbitMQ.Bus.Implement;
using StoreService.Mensajeria.Email.SendGridLibreria.Interface;
using StoreService.Mensajeria.Email.SendGridLibreria.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
});

builder.Services.AddSingleton<ISendGridEnviar, SendGridEnviar>();

builder.Services.AddTransient<EmailEventoManejador>();

builder.Services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoAutor>(options => 
{ 
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase")); 
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

var app = builder.Build();

// Configuración del Event Bus
using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IRabbitEventBus>();
    await eventBus.Subscribe<EmailEventoQueue, EmailEventoManejador>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
