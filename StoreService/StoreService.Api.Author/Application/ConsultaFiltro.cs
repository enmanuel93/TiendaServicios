using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.Author.Model;
using StoreService.Api.Author.Persistence;

namespace StoreService.Api.Author.Application
{
    public class ConsultaFiltro
    {
        public class AutorUnico: IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this._contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibros.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if(autor == null)
                {
                    throw new Exception("No se encontro el autor.");
                }

                var autorDto = mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
