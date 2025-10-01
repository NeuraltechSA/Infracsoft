using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax;

public class ImportOnPresuncionDigimaxImagesStored(
    ImportPresuncionDigimaxUseCase importPresuncionDigimaxUseCase
) : IConsumer<DigimaxImagesStoredEvent>
{
    private readonly ImportPresuncionDigimaxUseCase _importPresuncionDigimaxUseCase = importPresuncionDigimaxUseCase;
    
    public async Task Consume(ConsumeContext<DigimaxImagesStoredEvent> context)
    {
        await _importPresuncionDigimaxUseCase.Execute(context.Message.ImagenesIds.ToList(), context.Message.CompressedFileSourcePath);
    }
}
