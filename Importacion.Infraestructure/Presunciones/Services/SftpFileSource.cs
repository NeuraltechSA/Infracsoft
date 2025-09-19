using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Renci.SshNet;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Services;

public sealed class SftpFileSource : IPresuncionFileSource
{
    private SftpClient GetClient()
    {
        //TODO: From env
        return new SftpClient("neuralsys.com.ar",5379, "neuralsys_precarga", "yFy6K226Sh");
    }

    public async Task Delete(string path)
    {
        using var client = GetClient();
        await client.ConnectAsync(CancellationToken.None);
        await client.DeleteAsync(path);
    }

    public async Task<Stream> DownloadFile(string path)
    {
        using var client = GetClient();
        await client.ConnectAsync(CancellationToken.None);
        var stream = new MemoryStream();
        await Task.Factory.FromAsync(client.BeginDownloadFile(path, stream), client.EndDownloadFile);
        return stream;
    }

    public async Task<IEnumerable<string>> GetAllFilePathsRecursive()
    {
        using var client = GetClient();
        await client.ConnectAsync(CancellationToken.None);
        return await GetFilePathsFromDirectory(client, "/files/precarga/PRUEBAS_IGNORAR/");
    }

    private static async Task<IEnumerable<string>> GetFilePathsFromDirectory(SftpClient client, string path)
    {
        var currentFiles = client.ListDirectoryAsync(path, CancellationToken.None);
        var allFiles = new List<string>();
        await foreach (var file in currentFiles)
        {
            if (file.Name is "." or "..") continue;
            if (file.IsDirectory) allFiles.AddRange(await GetFilePathsFromDirectory(client, file.FullName));

            allFiles.Add(file.FullName);
        }
        return allFiles;
    }
}
