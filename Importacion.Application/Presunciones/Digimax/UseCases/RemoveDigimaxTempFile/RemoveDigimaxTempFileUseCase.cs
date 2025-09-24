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
    /// <param name="tempFilePath">Ruta el archivo temporal a eliminar.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Execute(string tempFilePath)
    {
        await _tempStore.DeleteFile(tempFilePath);
    }
}