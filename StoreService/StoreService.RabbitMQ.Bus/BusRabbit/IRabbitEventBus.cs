using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using StoreService.RabbitMQ.Bus.Comandos;
using StoreService.RabbitMQ.Bus.Eventos;

namespace StoreService.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task EnviarComando<T>(T comando) where T : Comando;

        Task Publish<T>(T evento) where T : Evento;
        Task Subscribe<T, TH>() where T : Evento 
                                where TH : IEventoManejador<T>;
    }
}