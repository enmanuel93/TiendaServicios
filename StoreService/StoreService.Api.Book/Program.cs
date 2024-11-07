using Microsoft.EntityFrameworkCore;
using StoreService.Api.Book.Persistence;
using MediatR;
using StoreService.Api.Book.Application;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
