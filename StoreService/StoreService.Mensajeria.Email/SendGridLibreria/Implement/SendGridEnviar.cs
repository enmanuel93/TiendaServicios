using SendGrid;
using SendGrid.Helpers.Mail;
using StoreService.Mensajeria.Email.SendGridLibreria.Interface;
using StoreService.Mensajeria.Email.SendGridLibreria.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.Mensajeria.Email.SendGridLibreria.Implement
{
    public class SendGridEnviar : ISendGridEnviar
    {
        public async Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data)
        {
			try
			{
                var sendGridCliente = new SendGridClient(data.SendGridAPIKey);
                var destinatario = new EmailAddress(data.emailDestinatario, data.NombreDestinatario);
                var tituloEmail = data.Titulo;
                var sender = new EmailAddress("cruzmejia9393@gmail.com", "Cruz Mejia");
                var contenidoMensaje = data.Contenido;

                var objMensaje = MailHelper.CreateSingleEmail(sender, destinatario, tituloEmail, contenidoMensaje, contenidoMensaje);
			    
                await sendGridCliente.SendEmailAsync(objMensaje);

                return (true, null);
            }
			catch (Exception e)
			{
                return (false, e.Message);
			}
        }
    }
}
