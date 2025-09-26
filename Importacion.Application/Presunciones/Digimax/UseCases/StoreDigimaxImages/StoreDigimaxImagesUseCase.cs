using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Imagenes.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages
{
    public sealed class StoreDigimaxImagesUseCase(
        IImagenStorageService imagenStorage,
        IPresuncionTempFileStore tempStore,
        IGuidGenerator guidGenerator,
        IEventBus eventBus,
        IUnitOfWork unitOfWork
    )
    {
        //private readonly PresuncionDigimaxImagenStore _presuncionDigimaxImagenStore = presuncionDigimaxImagenStore;
        private readonly IPresuncionTempFileStore _tempStore = tempStore;
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IEventBus _eventBus = eventBus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IImagenStorageService _imagenStorage = imagenStorage;

        public async Task Execute(string compressedFileTempPath, string baseTempPath, string compressedFileSourcePath)
        {
            try
            {
                //TODO: logging
                await Store(compressedFileTempPath, baseTempPath, compressedFileSourcePath);
            }
            catch (Exception e)
            {
                await _eventBus.Publish(new DigimaxImagesStorageFailedEvent(baseTempPath, compressedFileTempPath));
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task Store(string compressedFileTempPath, string baseTempPath, string compressedFileSourcePath)
        {
            var paths = await _tempStore.GetFilePathsFromFolder(baseTempPath);
            var ids = new List<string>();
            foreach (var imagePath in paths)
            {
                using var imageStream = await _tempStore.DownloadFile(imagePath);
                var filename = Path.GetFileName(imagePath);
                var imagenId = _guidGenerator.GenerateGuid().ToString();
                ids.Add(imagenId);
                await _imagenStorage.Upload(imagenId, filename, imageStream);
            }
            await _eventBus.Publish(
                new DigimaxImagesStoredEvent(ids, compressedFileSourcePath, compressedFileTempPath, baseTempPath)
            );

        }
    }
}
