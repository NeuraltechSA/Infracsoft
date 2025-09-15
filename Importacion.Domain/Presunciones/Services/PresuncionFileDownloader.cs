using Infracsoft.Importacion.Domain.Presunciones.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

/// <summary>
/// Servicio de dominio para la descarga y almacenamiento de archivos de presunciones
/// </summary>
public sealed class PresuncionFileDownloader
{
    private readonly IPresuncionSource _fileSource;
    private readonly IPresuncionStore _fileStore;

    public PresuncionFileDownloader(
        IPresuncionSource fileSource, 
        IPresuncionStore fileStore)
    {
        _fileSource = fileSource;
        _fileStore = fileStore;
    }

    /// <summary>
    /// Descarga y almacena una presunción completa con sus imágenes
    /// </summary>
    public async Task<PresuncionDownloadResult> DownloadPresuncionAsync(string presuncionPath)
    {
        // Descargar el archivo principal de la presunción
        var presuncionContent = await _fileSource.DownloadPresuncionFileAsync(presuncionPath);
        var presuncionFileName = Path.GetFileName(presuncionPath);
        var storedPresuncionPath = await _fileStore.StorePresuncionFileAsync(presuncionContent, presuncionFileName);

        // Obtener las rutas de las imágenes
        var imagePaths = await _fileSource.GetPresuncionImagesPathsAsync(presuncionPath);
        
        var storedImagePaths = new List<string>();
        
        // Descargar y almacenar cada imagen
        foreach (var imagePath in imagePaths)
        {
            var imageContent = await _fileSource.DownloadPresuncionImageAsync(imagePath);
            var imageFileName = Path.GetFileName(imagePath);
            var storedImagePath = await _fileStore.StorePresuncionImageAsync(imageContent, imageFileName);
            storedImagePaths.Add(storedImagePath);
        }

        return new PresuncionDownloadResult(
            presuncionPath, 
            storedPresuncionPath, 
            storedImagePaths);
    }

    /// <summary>
    /// Descarga todas las presunciones disponibles
    /// </summary>
    public async Task<IEnumerable<PresuncionDownloadResult>> DownloadAllPresuncionesAsync()
    {
        var presuncionPaths = await _fileSource.GetPresuncionesPathsAsync();
        var results = new List<PresuncionDownloadResult>();

        foreach (var presuncionPath in presuncionPaths)
        {
            var result = await DownloadPresuncionAsync(presuncionPath);
            results.Add(result);
        }

        return results;
    }
}

/// <summary>
/// Resultado de la descarga de una presunción
/// </summary>
public record PresuncionDownloadResult(
    string OriginalPath,
    string StoredPresuncionPath,
    IEnumerable<string> StoredImagePaths);
