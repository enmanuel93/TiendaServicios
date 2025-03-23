using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreService.RabbitMQ.Bus.BusRabbit;
using StoreService.RabbitMQ.Bus.EventoQueue;

namespace StoreService.Api.Author.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;

        public EmailEventoManejador() { }

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation(@event.Titulo);
            return Task.CompletedTask;
        }
    }
}