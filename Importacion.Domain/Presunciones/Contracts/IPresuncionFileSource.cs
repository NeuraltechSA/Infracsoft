
namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{
    public interface IPresuncionFileSource
    {
        //public IAsyncEnumerable<NamedStream> GetPresuncionFiles(string presuncionPath);
        public Task<IEnumerable<string>> GetAllFilePathsRecursive();
        public Task<Stream> DownloadFile(string path);
        public Task Delete(string path);
    }
}
