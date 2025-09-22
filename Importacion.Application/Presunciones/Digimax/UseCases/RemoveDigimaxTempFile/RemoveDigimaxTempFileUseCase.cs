using Infracsoft.Importacion.Domain.Presunciones.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.RemoveDigimaxTempFile;

/// <summary>
/// Caso de uso para eliminar archivos temporales de Digimax.
/// Elimina archivos temporales comprimidos y descomprimidos.
/// </summary>
public class RemoveDigimaxTempFileUseCase(IPresuncionTempStore tempStore)
{
    private readonly IPresuncionTempStore _tempStore = tempStore;

    /// <summary>
    /// Ejecuta la eliminación de archivos temporales del directorio base.
    /// </summary>
    /// <param name="basePath">Ruta base del directorio a limpiar.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Execute(string basePath)
    {
        // Eliminar todos los archivos del directorio base
        var allPaths = await _tempStore.GetFilePathsFromFolder(basePath);
        foreach (var path in allPaths)
        {
            await _tempStore.DeleteFile(path);
        }
        
        // Eliminar el directorio base si está vacío
        await _tempStore.DeleteFolder(basePath);
    }
}