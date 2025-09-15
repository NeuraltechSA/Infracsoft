using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Infraestructure.Presunciones.Contracts;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Services
{
    public sealed class SftpFileSource() : IPresuncionSource
    {
        public async Task<IEnumerable<string>> GetPresuncionesPathsAsync()
        {
            using var client = GetClient();
            client.Connect();
            var files = await GetFilesRecursively(client, "/");
            return files.Where(f => !f.IsDirectory && f.Name.EndsWith(".json")).Select(file => file.FullName);
        }

        public async Task<IEnumerable<string>> GetPresuncionImagesPathsAsync(string presuncionPath)
        {
            using var client = GetClient();
            client.Connect();
            
            var directory = Path.GetDirectoryName(presuncionPath);
            var files = await GetFilesRecursively(client, directory ?? "/");
            return files.Where(f => !f.IsDirectory && IsImageFile(f.Name)).Select(file => file.FullName);
        }

        public async Task<byte[]> DownloadPresuncionFileAsync(string remotePath)
        {
            using var client = GetClient();
            client.Connect();
            
            using var memoryStream = new MemoryStream();
            await Task.Factory.FromAsync(client.BeginDownloadFile(remotePath, memoryStream), client.EndDownloadFile);
            return memoryStream.ToArray();
        }

        public async Task<byte[]> DownloadPresuncionImageAsync(string remotePath)
        {
            using var client = GetClient();
            client.Connect();
            
            using var memoryStream = new MemoryStream();
            await Task.Factory.FromAsync(client.BeginDownloadFile(remotePath, memoryStream), client.EndDownloadFile);
            return memoryStream.ToArray();
        }

        private bool IsImageFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp";
        }

        private async Task<List<ISftpFile>> GetFilesRecursively(SftpClient client, string path)
        {
            var currentFiles = client.ListDirectoryAsync(path, CancellationToken.None); //TODO: Add CancellationToken
            var totalFiles = new List<ISftpFile>();
            await foreach (var file in currentFiles)
            {
                if (file.IsDirectory) totalFiles.AddRange(await GetFilesRecursively(client, file.FullName));
                totalFiles.Add(file);
            }
            return totalFiles;
        }

        private SftpClient GetClient()
        {
            //TODO: From env
            return new SftpClient("neuralsys.com.ar", "neuralsys_precarga", "yFy6K226Sh");
        }
    }
}
