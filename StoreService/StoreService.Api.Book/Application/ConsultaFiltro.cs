using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.Book.DTos;
using StoreService.Api.Book.Models;
using StoreService.Api.Book.Persistence;

namespace StoreService.Api.Book.Application
{
    public class ConsultaFiltro
    {
        public class LibroUnico: IRequest<LibreriaMaterialDto> {
            public Guid? LibroId { get; set; }
        }

        public class Menejador : IRequestHandler<LibroUnico, LibreriaMaterialDto>
        {
            private readonly LibreriaContext libreriaContext;
            private readonly IMapper mapper;

            public Menejador(LibreriaContext libreriaContext, IMapper mapper)
            {
                this.libreriaContext = libreriaContext;
                this.mapper = mapper;
            }

            public async Task<LibreriaMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await libreriaContext.LibreriaMaterials.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();

                if (libro == null)
                {
                    throw new Exception("No se encontro el libro!");
                }

                return mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);
            }
        }
    }
}
