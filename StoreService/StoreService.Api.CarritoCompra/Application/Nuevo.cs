using MediatR;
using StoreService.Api.CarritoCompra.Models;
using StoreService.Api.CarritoCompra.Persistence;

namespace StoreService.Api.CarritoCompra.Application
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContext _context;

            public Manejador(CarritoContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _context.CarritoSesions.Add(carritoSesion);

                var value = await _context.SaveChangesAsync();

                if(value == 0)
                {
                    throw new Exception("Errores en la inserción del carrito!");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach(var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };

                    _context.CarritoSesionDetalles.Add(detalleSesion);
                }

                value = await _context.SaveChangesAsync();

                if(value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle carrito de compra!");
            }
        }
    }
}
