﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.Mensajeria.Email.SendGridLibreria.Modelo
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; }
        
        public string emailDestinatario { get; set; }

        public string NombreDestinatario { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }
    }
}
