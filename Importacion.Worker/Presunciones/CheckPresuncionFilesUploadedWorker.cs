namespace Importacion.Worker.Presunciones;

public class CheckPresuncionFilesUploadedWorker : BackgroundService
{
    private readonly ILogger<CheckPresuncionFilesUploadedWorker> _logger;

    public CheckPresuncionFilesUploadedWorker(ILogger<CheckPresuncionFilesUploadedWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
