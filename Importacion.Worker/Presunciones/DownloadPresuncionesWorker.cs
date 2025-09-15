using Infracsoft.Importacion.Application.Presunciones.UseCases;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Importacion.Worker.Presunciones;

/// <summary>
/// Worker que descarga presunciones desde la fuente remota y las almacena localmente
/// </summary>
public sealed class DownloadPresuncionesWorker : BackgroundService
{
    private readonly DownloadPresuncionesUseCase _downloadUseCase;
    private readonly ILogger<DownloadPresuncionesWorker> _logger;
    private readonly TimeSpan _period = TimeSpan.FromMinutes(30); // Ejecutar cada 30 minutos

    public DownloadPresuncionesWorker(
        DownloadPresuncionesUseCase downloadUseCase,
        ILogger<DownloadPresuncionesWorker> logger)
    {
        _downloadUseCase = downloadUseCase;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Iniciando descarga de presunciones...");
                
                var results = await _downloadUseCase.ExecuteAsync();
                
                _logger.LogInformation("Descarga completada. {Count} presunciones procesadas", results.Count());
                
                foreach (var result in results)
                {
                    _logger.LogInformation(
                        "Presunción descargada: {OriginalPath} -> {StoredPath} con {ImageCount} imágenes",
                        result.OriginalPath,
                        result.StoredPresuncionPath,
                        result.StoredImagePaths.Count());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la descarga de presunciones");
            }

            await Task.Delay(_period, stoppingToken);
        }
    }
}
