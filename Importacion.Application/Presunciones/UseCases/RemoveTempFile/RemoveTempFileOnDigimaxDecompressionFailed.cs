using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Application.Presunciones.UseCases.RemoveTempFile;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.RemoveTempFile;

/// <summary>
/// Event handler que reacciona al evento DigimaxDecompressionFailedEvent.
/// Este handler limpia el archivo temporal cuando falla la descompresión de archivos Digimax.
/// </summary>
public class RemoveTempFileOnDigimaxDecompressionFailed(
    RemoveTempFileUseCase removeTempFileUseCase
) : IConsumer<DigimaxDecompressionFailedEvent>
{
    private readonly RemoveTempFileUseCase _removeTempFileUseCase = removeTempFileUseCase;
    
    public async Task Consume(ConsumeContext<DigimaxDecompressionFailedEvent> context)
    {
        await _removeTempFileUseCase.Execute(context.Message.TempFilePath);
    }
}
