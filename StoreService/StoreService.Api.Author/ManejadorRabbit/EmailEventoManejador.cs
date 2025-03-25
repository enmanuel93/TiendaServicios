using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreService.Mensajeria.Email.SendGridLibreria.Interface;
using StoreService.Mensajeria.Email.SendGridLibreria.Modelo;
using StoreService.RabbitMQ.Bus.BusRabbit;
using StoreService.RabbitMQ.Bus.EventoQueue;

namespace StoreService.Api.Author.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly IConfiguration _configuration;

        public EmailEventoManejador() { }

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGridEnviar, IConfiguration configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation(@event.Titulo);
            var objData = new SendGridData();
            objData.Contenido = @event.Contenido;
            objData.emailDestinatario = @event.Destinatario;
            objData.NombreDestinatario = @event.Destinatario;
            objData.Titulo = @event.Titulo;
            objData.SendGridAPIKey = _configuration["SendGrid:ApiKey"];

            var resultado = await _sendGridEnviar.EnviarEmail(objData);

            if (resultado.resultado)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}