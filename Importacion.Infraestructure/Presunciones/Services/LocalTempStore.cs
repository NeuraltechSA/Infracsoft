using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.Infrastructure
{
    /// <summary>
    /// Implementación local de almacenamiento temporal para presunciones.
    /// Utiliza el sistema de archivos local para almacenar y recuperar archivos temporales.
    /// </summary>
    public sealed class LocalTempStore : IPresuncionTempStore
    {
       
        private string _tempPath => Path.GetTempPath();

        private void EnsureDirectory(string fullPath)
        {
            var directory = Path.GetDirectoryName(fullPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public async Task Store(string path, Stream content)
        {
            var fullPath = Path.Combine(_tempPath, path);
            EnsureDirectory(fullPath);
            using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            content.Seek(0, SeekOrigin.Begin);
            await content.CopyToAsync(fileStream);
        }

        public async Task<Stream> DownloadFile(string path)
        {
            var fullPath = Path.Combine(_tempPath, path);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"El archivo temporal no existe: {fullPath}");
            }

            // Abrimos el archivo en modo solo lectura y lo devolvemos como Stream.
            // El consumidor es responsable de disponer el stream.
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await Task.FromResult(fileStream);
        }

        public Task Delete(string path)
        {
            var fullPath = Path.Combine(_tempPath, path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<string>> GetFilePathsFromFolder(string directoryPath)
        {
            var fullPath = Path.Combine(_tempPath, directoryPath);
            if (!Directory.Exists(fullPath))
            {
                return Enumerable.Empty<string>();
            }
            return await Task.FromResult(Directory.GetFiles(fullPath));
        }
    }
}