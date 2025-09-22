using Infracsoft.Importacion.Domain.Presunciones.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.RemoveDigimaxTempImages;

/// <summary>
/// Caso de uso para limpiar archivos temporales cuando falla el almacenamiento de imágenes de Digimax.
/// Elimina todos los archivos temporales del directorio base y el directorio mismo.
/// </summary>
public class RemoveDigimaxTempImagesUseCase(IPresuncionTempStore tempStore)
{
    private readonly IPresuncionTempStore _tempStore = tempStore;

    /// <summary>
    /// Ejecuta la limpieza de archivos temporales del directorio base.
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
