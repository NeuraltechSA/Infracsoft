using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax;

public class ImportOnPresuncionDigimaxUploaded(
    ImportPresuncionDigimaxUseCase importPresuncionDigimaxUseCase
) : IConsumer<PresuncionDigimaxUploadedEvent>
{
    private readonly ImportPresuncionDigimaxUseCase _importPresuncionDigimaxUseCase = importPresuncionDigimaxUseCase;
    public async Task Consume(ConsumeContext<PresuncionDigimaxUploadedEvent> context)
    {
        await _importPresuncionDigimaxUseCase.Execute(context.Message.CompressedFileSourcePath);
    }
}
