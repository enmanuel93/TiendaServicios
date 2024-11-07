using FluentValidation;
using MediatR;
using StoreService.Api.Author.Model;
using StoreService.Api.Author.Persistence;

namespace StoreService.Api.Author.Application
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidation: AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor contexto;

            public Manejador(ContextoAutor contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };

                contexto.AutorLibros.Add(autorLibro);
                var valor = await contexto.SaveChangesAsync();

                if(valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Autor del libro!");
            }
        }
    }
}
