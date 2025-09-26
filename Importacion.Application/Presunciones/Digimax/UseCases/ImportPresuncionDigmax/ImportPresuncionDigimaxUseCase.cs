using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax
{
    public class ImportPresuncionDigimaxUseCase(
        IGuidGenerator guidGenerator,
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    )
    {
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;

        public async Task Execute(List<string> imagenes, string compressedFileSourcePath)
        {
            try
            {
                var presuncionId = _guidGenerator.GenerateGuid().ToString();
                var presuncion = PresuncionVelocidad.ImportFromDigimax(presuncionId, imagenes, compressedFileSourcePath);

                await _repository.Create(presuncion);
                await _eventBus.Publish(presuncion.PullDomainEvents());
            }
            catch (Exception)
            {
                await _eventBus.Publish(new PresuncionImportFailedEvent(compressedFileSourcePath));
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
