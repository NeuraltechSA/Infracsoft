using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile;

public class StoreTempFileOnPresuncionDigimaxImported(
    StoreDigimaxTempFileUseCase storeDigimaxTempCompressedFileUseCase
) : IConsumer<PresuncionDigimaxImportedEvent>
{
    private readonly StoreDigimaxTempFileUseCase _storeDigimaxTempCompressedFileUseCase = storeDigimaxTempCompressedFileUseCase;

    public async Task Consume(ConsumeContext<PresuncionDigimaxImportedEvent> context)
    {
        await _storeDigimaxTempCompressedFileUseCase.Execute(
            context.Message.CompressedFileSourcePath, 
            context.Message.PresuncionId
        );
    }

}
