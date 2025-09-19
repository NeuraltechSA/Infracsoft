using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile
{
    public class StoreDigimaxTempFileUseCase(
        IPresuncionFileSource fileSource, 
        IPresuncionTempStore tempStore,
        IEventBus eventBus
    )
    {
        private readonly IPresuncionFileSource _fileSource = fileSource;
        private readonly IPresuncionTempStore _tempStore = tempStore;
        private readonly IEventBus _eventBus = eventBus;
        public async Task Execute(string compressedFileSourcePath, string presuncionId)
        {
            using var stream = await _fileSource.DownloadFile(compressedFileSourcePath);
            var fileName = Path.GetFileName(compressedFileSourcePath);
            await _tempStore.Store(fileName, stream);
            await _eventBus.Publish(new DigimaxTempFileStoredEvent(fileName, presuncionId));
        }
    }
}
