using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using SharedKernel.Domain.Utilities;

namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{
    public interface IPresuncionSource
    {
        public Task<IEnumerable<string>> GetPresuncionesPaths();
        public IAsyncEnumerable<NamedStream> GetPresuncionFiles(string presuncionPath);
        public Task DeletePresuncion(string presuncionPath);
    }
}
