using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreService.RabbitMQ.Bus.Eventos;

namespace StoreService.RabbitMQ.Bus.Comandos
{
    public abstract class Comando : Message
    {
        public DateTime Timestamp {get; protected set;}

        protected Comando(){
            Timestamp = DateTime.Now;
        }
    }
}