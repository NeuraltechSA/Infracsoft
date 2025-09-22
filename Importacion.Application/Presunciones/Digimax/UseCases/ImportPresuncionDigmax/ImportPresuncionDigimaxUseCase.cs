using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax
{
    public class ImportPresuncionDigimaxUseCase(
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    )
    {
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;

        public async Task Execute(string compressedFileSourcePath, string presuncionId)
        {
            try
            {
                var presuncion = PresuncionVelocidad.ImportFromDigimax(presuncionId, compressedFileSourcePath);

                await _repository.Create(presuncion);
                await _eventBus.Publish(presuncion.PullDomainEvents());
            }
            catch (Exception)
            {
                await _eventBus.Publish(new PresuncionImportFailedEvent(
                    presuncionId, 
                    compressedFileSourcePath));
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
