using StoreService.Api.CarritoCompra.RemoteModel;

namespace StoreService.Api.CarritoCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
