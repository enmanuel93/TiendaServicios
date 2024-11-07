using StoreService.Api.CarritoCompra.RemoteInterface;
using StoreService.Api.CarritoCompra.RemoteModel;
using System.Text.Json;

namespace StoreService.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibroService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClientFactory, ILogger<LibrosService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }


        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);

                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
