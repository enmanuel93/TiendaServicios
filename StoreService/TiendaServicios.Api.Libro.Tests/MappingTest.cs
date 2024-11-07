using AutoMapper;
using StoreService.Api.Book.DTos;
using StoreService.Api.Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest: Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();
        }
    }
}
