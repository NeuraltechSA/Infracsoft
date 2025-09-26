using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Application.Presunciones.UseCases.CleanTempPath;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.CleanTempPath;

/// <summary>
/// Event handler que reacciona al evento DigimaxImagesStorageFailedEvent.
/// Este handler limpia el directorio temporal cuando falla el almacenamiento de imágenes de Digimax.
/// </summary>
public class CleanTempPathOnDigimaxImagesStorageFailed(
    CleanTempPathUseCase cleanTempPathUseCase
) : IConsumer<DigimaxImagesStorageFailedEvent>
{
    private readonly CleanTempPathUseCase _cleanTempPathUseCase = cleanTempPathUseCase;
    
    public async Task Consume(ConsumeContext<DigimaxImagesStorageFailedEvent> context)
    {
        await _cleanTempPathUseCase.Execute(context.Message.TempBasePath);
    }
}
