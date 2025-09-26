using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.CleanTempPath;

public sealed class CleanTempPathUseCase(
    IPresuncionTempFileStore tempStore
)
{
    private readonly IPresuncionTempFileStore _tempStore = tempStore;

    public async Task Execute(string path)
    {
        //TODO: try catch, logging
        await Clean(path);
    }

    private async Task Clean(string path)
    {
        // Eliminar todos los archivos del directorio
        var allPaths = await _tempStore.GetFilePathsFromFolder(path);
        foreach (var filePath in allPaths)
        {
            await _tempStore.DeleteFile(filePath);
        }
        
        // Eliminar el directorio si está vacío
        await _tempStore.DeleteFolder(path);
    }
}
