using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using SharedKernel.Domain.Utilities;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Services;

/// <summary>
/// Implementación del almacén de archivos usando el sistema de archivos local
/// </summary>
public sealed class LocalTempFileStore : IPresuncionTempStore
{
    /*
    private readonly string _basePath;

    public LocalTempFileStore(string basePath)
    {
        _basePath = basePath;
        EnsureDirectoryExists(_basePath);
    }
    private void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }*/

    public string TmpPath
    {
        get
        {
            return Path.GetTempPath();
        }
    }

    private void EnsureDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public async Task Store(string destinationPath, NamedStream stream)
    {
        var filePath = Path.Combine(TmpPath, destinationPath);
        EnsureDirectory(filePath);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream);
    }

    public Task DeletePresuncion(string destinationPath)
    {
        var filePath = Path.Combine(TmpPath, destinationPath);
        if (Directory.Exists(filePath))
        {
            Directory.Delete(filePath, true);
        }
        return Task.CompletedTask;
    }

    public async Task<string> GetRawPresuncionData(string destinationPath)
    {
        var filePath = Path.Combine(TmpPath, destinationPath);
        if (File.Exists(filePath))
        {
            return await File.ReadAllTextAsync(filePath);
        }
        return string.Empty;
    }

    public async IAsyncEnumerable<NamedStream> GetPresuncionImages(string destinationPath)
    {
        var directoryPath = Path.Combine(TmpPath, destinationPath);
        if (Directory.Exists(directoryPath))
        {
            var imageFiles = Directory.GetFiles(directoryPath, "*.jpg")
                .Concat(Directory.GetFiles(directoryPath, "*.jpeg"))
                .Concat(Directory.GetFiles(directoryPath, "*.png"))
                .Concat(Directory.GetFiles(directoryPath, "*.gif"))
                .Concat(Directory.GetFiles(directoryPath, "*.bmp"));

            foreach (var imageFile in imageFiles)
            {
                var fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read);
                yield return new NamedStream(fileStream, imageFile);
            }
        }
        await Task.CompletedTask;
    }

    public async IAsyncEnumerable<NamedStream> GetPresuncionVideos(string destinationPath)
    {
        var directoryPath = Path.Combine(TmpPath, destinationPath);
        if (Directory.Exists(directoryPath))
        {
            var videoFiles = Directory.GetFiles(directoryPath, "*.mkv");

            foreach (var videoFile in videoFiles)
            {
                var fileStream = new FileStream(videoFile, FileMode.Open, FileAccess.Read);
                yield return new NamedStream(fileStream, videoFile);
            }
        }
        await Task.CompletedTask;
    }

    /*
    public async Task<string> StorePresuncionFileAsync(byte[] content, string fileName)
    {
        var filePath = Path.Combine(_basePath, "presunciones", fileName);
        await StoreFileAsync(content, filePath);
        return filePath;
    }

    public async Task<string> StorePresuncionImageAsync(byte[] content, string fileName)
    {
        var filePath = Path.Combine(_basePath, "imagenes", fileName);
        await StoreFileAsync(content, filePath);
        return filePath;
    }

    public async Task<byte[]> GetFileContentAsync(string filePath)
    {
        return await File.ReadAllBytesAsync(filePath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        return Task.FromResult(File.Exists(filePath));
    }

    private async Task StoreFileAsync(byte[] content, string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            EnsureDirectoryExists(directory);
        }

        await File.WriteAllBytesAsync(filePath, content);
    }

    private void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public Task Store(string presuncionKey, NamedStream stream)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRawPresuncionData(string presuncionKey)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<NamedStream> GetPresuncionImages(string presuncionKey)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<NamedStream> GetPresuncionVideos(string presuncionKey)
    {
        throw new NotImplementedException();
    }

    public Task DeletePresuncion(string presuncionKey)
    {
        throw new NotImplementedException();
    }*/
}
