using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.RemoveTempFile;

public sealed class RemoveTempFileUseCase(
    IPresuncionTempFileStore tempStore
)
{
    private readonly IPresuncionTempFileStore _tempStore = tempStore;

    public async Task Execute(string filePath)
    {
        //TODO: try catch, logging
        await Remove(filePath);
    }

    private async Task Remove(string filePath)
    {
        await _tempStore.DeleteFile(filePath);
    }
}
