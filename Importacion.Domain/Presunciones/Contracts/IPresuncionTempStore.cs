using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{

    public interface IPresuncionTempStore
    {
        public Task<IEnumerable<string>> GetFilePathsFromFolder(string directoryPath);
        public Task Store(string destinationPath, Stream stream);
        public Task<Stream> DownloadFile(string path);
        public Task DeleteFile(string path);
        public Task DeleteFolder(string folderPath);
    }
}
