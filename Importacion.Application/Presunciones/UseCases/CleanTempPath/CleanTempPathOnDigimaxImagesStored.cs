using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Application.Presunciones.UseCases.CleanTempPath;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.CleanTempPath;

/// <summary>
/// Event handler que reacciona al evento DigimaxImagesStoredEvent.
/// Este handler limpia el directorio temporal cuando las imágenes de Digimax han sido almacenadas exitosamente.
/// </summary>
public class CleanTempPathOnDigimaxImagesStored(
    CleanTempPathUseCase cleanTempPathUseCase
) : IConsumer<DigimaxImagesStoredEvent>
{
    private readonly CleanTempPathUseCase _cleanTempPathUseCase = cleanTempPathUseCase;
    
    public async Task Consume(ConsumeContext<DigimaxImagesStoredEvent> context)
    {
        await _cleanTempPathUseCase.Execute(context.Message.TempBasePath);
    }
}
