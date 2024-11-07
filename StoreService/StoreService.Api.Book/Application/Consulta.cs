using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.Book.DTos;
using StoreService.Api.Book.Models;
using StoreService.Api.Book.Persistence;

namespace StoreService.Api.Book.Application
{
    public class Consulta
    {
        public class Ejecuta: IRequest<List<LibreriaMaterialDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {
            private readonly LibreriaContext libreriaContext;
            private readonly IMapper mapper;

            public Manejador(LibreriaContext libreriaContext, IMapper mapper)
            {
                this.libreriaContext = libreriaContext;
                this.mapper = mapper;
            }

            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await libreriaContext.LibreriaMaterials.ToListAsync();

                var librosDto = mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);

                return librosDto;
            }
        }
    }
}
