using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax;

public class ImportOnPresuncionDigimaxImagesStored(
    ImportPresuncionDigimaxUseCase importPresuncionDigimaxUseCase
) : IConsumer<PresuncionDigimaxImagesStoredEvent>
{
    private readonly ImportPresuncionDigimaxUseCase _importPresuncionDigimaxUseCase = importPresuncionDigimaxUseCase;
    
    public async Task Consume(ConsumeContext<PresuncionDigimaxImagesStoredEvent> context)
    {
        await _importPresuncionDigimaxUseCase.Execute(context.Message.CompressedFileSourcePath, context.Message.PresuncionId);
    }
}
