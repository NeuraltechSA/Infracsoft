using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

/// <summary>
/// Servicio específico para el almacenamiento de imágenes de presunciones Digimax.
/// Encapsula las reglas de negocio específicas de Digimax para el manejo de imágenes.
/// </summary>
public sealed class PresuncionDigimaxImagenStore(
    IPresuncionTempStore tempStore,
    IGuidGenerator guidGenerator,
    ImagenStore imagenStore
)
{
    private readonly IPresuncionTempStore _tempStore = tempStore;
    private readonly IGuidGenerator _guidGenerator = guidGenerator;
    private readonly ImagenStore _imagenStore = imagenStore;

    /// <summary>
    /// Almacena las imágenes de una presunción Digimax desde un directorio temporal.
    /// Valida que existan exactamente 2 imágenes como requiere el protocolo Digimax.
    /// </summary>
    /// <param name="basePath">Ruta base del directorio temporal con las imágenes</param>
    /// <param name="presuncionId">ID de la presunción a la que pertenecen las imágenes</param>
    /// <exception cref="InvalidOperationException">Se lanza cuando no se encuentran exactamente 2 imágenes</exception>
    public async Task StoreImages(string basePath, string presuncionId)
    {
        var imagePaths = await GetImagePaths(basePath);
        ValidateDigimaxImageIntegrity(imagePaths);
        
        foreach (var imagePath in imagePaths)
        {
            var imageId = _guidGenerator.GenerateGuid().ToString();
            using var imageStream = await _tempStore.DownloadFile(imagePath);
            var filename = Path.GetFileName(imagePath);
            
            await _imagenStore.Store(imageId, presuncionId, filename, imageStream);
            await _tempStore.Delete(imagePath);
        }
    }

    /// <summary>
    /// Obtiene las rutas de las imágenes BMP del directorio especificado.
    /// </summary>
    /// <param name="basePath">Ruta base del directorio</param>
    /// <returns>Lista de rutas de archivos BMP</returns>
    private async Task<IEnumerable<string>> GetImagePaths(string basePath)
    {
        var allPaths = await _tempStore.GetFilePathsFromFolder(basePath);
        return allPaths.Where(p => p.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Valida que el número de imágenes cumpla con los requisitos específicos de Digimax.
    /// Los radares Digimax capturan exactamente 2 imágenes por presunción.
    /// </summary>
    /// <param name="imagePaths">Rutas de las imágenes a validar</param>
    /// <exception cref="InvalidDigimaxImageCountException">Se lanza cuando no se encuentran exactamente 2 imágenes</exception>
    private static void ValidateDigimaxImageIntegrity(IEnumerable<string> imagePaths)
    {
        var imageCount = imagePaths.Count();

        if (imageCount != 2)
        {
            throw InvalidDigimaxImageCountException.Create(2, imageCount);
        }
    }
}
