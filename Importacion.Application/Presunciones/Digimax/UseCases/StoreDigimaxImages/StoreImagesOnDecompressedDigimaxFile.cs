using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages;

public class StoreImagesOnDecompressedDigimaxFile(
    StoreDigimaxImagesUseCase storeImportedDigimaxPresuncionImagesUseCase
) : IConsumer<DecompressedDigimaxFileEvent>
{
    private readonly StoreDigimaxImagesUseCase _storeImportedDigimaxPresuncionImagesUseCase = storeImportedDigimaxPresuncionImagesUseCase;

    public async Task Consume(ConsumeContext<DecompressedDigimaxFileEvent> context)
    { 
        await _storeImportedDigimaxPresuncionImagesUseCase.Execute(
            context.Message.BasePath,
            context.Message.PresuncionId
        );
    }
}
