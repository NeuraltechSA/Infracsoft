using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.RemoveDigimaxTempFile;

/// <summary>
/// Event handler que reacciona al evento DigimaxDecompressionFailedEvent.
/// Este handler ejecuta el caso de uso de limpieza cuando falla la descompresión.
/// </summary>
public class RemoveTempFileOnDigimaxDecompressionFailed(RemoveDigimaxTempFileUseCase useCase) : IConsumer<DigimaxDecompressionFailedEvent>
{
    private readonly RemoveDigimaxTempFileUseCase _useCase = useCase;

    /// <summary>
    /// Procesa el evento de fallo en descompresión.
    /// Ejecuta el caso de uso de limpieza del archivo temporal comprimido.
    /// </summary>
    /// <param name="context">Contexto del evento con la ruta del archivo temporal.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Consume(ConsumeContext<DigimaxDecompressionFailedEvent> context)
    {
        await _useCase.Execute(context.Message.TempFilePath);
    }
}

