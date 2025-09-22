using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Imagenes.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Imagenes.UseCases.DeleteImagen;

/// <summary>
/// Caso de uso para eliminar una imagen del sistema.
/// Elimina la imagen del almacenamiento y de la base de datos.
/// </summary>
public class DeleteImagenUseCase(
    ImagenStore store
)
{
    private readonly ImagenStore _store = store;

    /// <summary>
    /// Ejecuta la eliminación de una imagen por su ID.
    /// </summary>
    /// <param name="imagenId">ID de la imagen a eliminar.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Execute(string imagenId)
    {
        await _store.Delete(imagenId);
    }
}
