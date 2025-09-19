using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.CheckSourceDigimax;

namespace Infracsoft.Importacion.Worker.Presunciones;

public class CheckPresuncionFilesUploadedWorker(
    ILogger<CheckPresuncionFilesUploadedWorker> logger,
    IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
    private readonly ILogger<CheckPresuncionFilesUploadedWorker> _logger = logger;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //while (!stoppingToken.IsCancellationRequested)
        //{
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }


            //     private readonly CheckPresuncionesOnSourceDigimaxUseCase _checkPresuncionesOnSourceDigimaxUseCase = checkPresuncionesOnSourceDigimaxUseCase;

            using var scope = _serviceScopeFactory.CreateScope();
            var checkPresuncionesOnSourceDigimaxUseCase = scope.ServiceProvider.GetRequiredService<CheckSourceDigimaxUseCase>();
            await checkPresuncionesOnSourceDigimaxUseCase.Execute();

            //await Task.Delay(1000, stoppingToken);
        //}
    }
}
