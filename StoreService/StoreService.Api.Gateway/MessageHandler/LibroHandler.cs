using System.Diagnostics;
using System.Text.Json;
using StoreService.Api.Gateway.InterfaceRemote;
using StoreService.Api.Gateway.LibroRemote;

namespace StoreService.Api.Gateway.MessageHandler
{
    public class LibroHandler : DelegatingHandler
    {
        private readonly IAutorRemote _autorRemote;
        private readonly ILogger<LibroHandler> _logger;

        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            this._logger = logger;
            this._autorRemote = autorRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tiempo = Stopwatch.StartNew();
            _logger.LogInformation("Inicia el request");
            var response = await base.SendAsync(request, cancellationToken);   

            if (response.IsSuccessStatusCode) {
                var contenido = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<LibroModeloRemote>(contenido, options);
                var responseAutor = await _autorRemote.GetAutor(resultado.AutorLibro ?? Guid.Empty);

                if (responseAutor.resultado) {
                    var objectoAutor = responseAutor.autor;
                    resultado.AutorData = objectoAutor;
                    var resultadoStr = JsonSerializer.Serialize(resultado);
                    response.Content = new StringContent(resultadoStr, System.Text.Encoding.UTF8, "application/json");
                }
            }


            _logger.LogInformation($"Este proceso se hizo en {tiempo.ElapsedMilliseconds} ms");

            return response;
        }
    }
}