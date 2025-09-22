using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.RemoveDigimaxTempImages;

/// <summary>
/// Event handler que reacciona al evento DigimaxImagesStorageFailedEvent.
/// Este handler ejecuta el caso de uso de limpieza cuando falla el almacenamiento de imágenes.
/// </summary>
public class RemoveTempImagesOnDigimaxImagesStorageFailed(RemoveDigimaxTempImagesUseCase useCase) : IConsumer<DigimaxImagesStorageFailedEvent>
{
    private readonly RemoveDigimaxTempImagesUseCase _useCase = useCase;

    /// <summary>
    /// Procesa el evento de fallo en almacenamiento de imágenes.
    /// Ejecuta el caso de uso de limpieza de archivos temporales.
    /// </summary>
    /// <param name="context">Contexto del evento con la ruta base del directorio.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Consume(ConsumeContext<DigimaxImagesStorageFailedEvent> context)
    {
        await _useCase.Execute(context.Message.BasePath);
    }
}

