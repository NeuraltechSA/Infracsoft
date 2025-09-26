using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Application.Presunciones.UseCases.RemoveTempFile;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.RemoveTempFile;

/// <summary>
/// Event handler que reacciona al evento DigimaxImagesStorageFailedEvent.
/// Este handler limpia el archivo temporal cuando falla el almacenamiento de imágenes de Digimax.
/// </summary>
public class RemoveTempFileOnDigimaxImagesStorageFailed(
    RemoveTempFileUseCase removeTempFileUseCase
) : IConsumer<DigimaxImagesStorageFailedEvent>
{
    private readonly RemoveTempFileUseCase _removeTempFileUseCase = removeTempFileUseCase;
    
    public async Task Consume(ConsumeContext<DigimaxImagesStorageFailedEvent> context)
    {
        await _removeTempFileUseCase.Execute(context.Message.TempFilePath);
    }
}
