using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.StoreImportedPresuncionFiles
{
    public class StoreImportedPresuncionImagesUseCase(
        ImagenStore imagenStore,
        IPresuncionTempStore fileStore,
        IGuidGenerator guidGenerator,
        IEventBus eventBus
    )
    {
        private readonly ImagenStore _imagenStore = imagenStore;
        private readonly IPresuncionTempStore _fileStore = fileStore;
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IEventBus _eventBus = eventBus;

        public async Task Execute(string sourcePath, string presuncionId)
        {
            var imagesPath = _fileStore.GetPresuncionImages(sourcePath);
            var imagesCount = 0;
            
            await foreach (var imageStream in imagesPath)
            {
                using var stream = imageStream;
                var imagenId = _guidGenerator.GenerateGuid().ToString();
                await _imagenStore.Store(imagenId, presuncionId, imageStream);
                imagesCount++;
            }

            // Disparar evento de imágenes almacenadas
            await _eventBus.Publish(new PresuncionImagesStoredEvent(presuncionId, remotePath, imagesCount));
        }


    }
}
