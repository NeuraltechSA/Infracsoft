using Infracsoft.Importacion.Domain.Presunciones.Services;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages
{
    public sealed class StoreDigimaxImagesUseCase(
        PresuncionDigimaxImagenStore presuncionDigimaxImagenStore,
        IEventBus eventBus,
        IUnitOfWork unitOfWork
    )
    {
        private readonly PresuncionDigimaxImagenStore _presuncionDigimaxImagenStore = presuncionDigimaxImagenStore;
        private readonly IEventBus _eventBus = eventBus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(string basePath, string originalSourcePath)
        {
            try
            {
                var presuncionId = await _presuncionDigimaxImagenStore.StoreImages(basePath, string.Empty);
                await _eventBus.Publish(new PresuncionDigimaxImagesStoredEvent(presuncionId, originalSourcePath));
            }
            catch (Exception e)
            {
                await _eventBus.Publish(new DigimaxImagesStorageFailedEvent(basePath));
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
