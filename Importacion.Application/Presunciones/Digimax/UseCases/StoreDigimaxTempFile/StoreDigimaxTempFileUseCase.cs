using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile
{
    public class StoreDigimaxTempFileUseCase(
        IPresuncionFileSource fileSource, 
        IPresuncionTempFileStore tempStore,
        IEventBus eventBus,
        IUnitOfWork unitOfWork
    )
    {
        private readonly IPresuncionFileSource _fileSource = fileSource;
        private readonly IPresuncionTempFileStore _tempStore = tempStore;
        private readonly IEventBus _eventBus = eventBus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Execute(string compressedFileSourcePath)
        {
            using var stream = await _fileSource.DownloadFile(compressedFileSourcePath);
            var fileName = Path.GetFileName(compressedFileSourcePath);
            await _tempStore.Store(fileName, stream);
            await _eventBus.Publish(new DigimaxTempFileStoredEvent(fileName,  compressedFileSourcePath));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
