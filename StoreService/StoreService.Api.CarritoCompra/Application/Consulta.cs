using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.CarritoCompra.Persistence;
using StoreService.Api.CarritoCompra.RemoteInterface;

namespace StoreService.Api.CarritoCompra.Application
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto>{
            public int CarritoSessionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContext context;
            private readonly ILibroService libroService;

            public Manejador(ILibroService libroService, CarritoContext context)
            {
                this.libroService = libroService;
                this.context = context;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await context.CarritoSesions.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSessionId);
                var carritoSesionDetalle =await context.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var objetoLibro = response.Libro;

                        listaCarritoDto.Add(new CarritoDetalleDto
                        {
                            AutorLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        });
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}
