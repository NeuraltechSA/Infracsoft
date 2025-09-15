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

    public async Task Store(string presuncionKey, NamedStream stream)
    {
        var filePath = Path.Combine(TmpPath, presuncionKey);
        EnsureDirectory(filePath);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream);
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
