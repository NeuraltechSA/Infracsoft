using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax
{
    public class ImportPresuncionDigimaxUseCase(
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        IGuidGenerator guidGenerator
    )
    {
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;

        public async Task Execute(string compressedFileSourcePath)
        {
            try
            {
                var id = _guidGenerator.GenerateGuid().ToString();
                var presuncion = PresuncionVelocidad.ImportFromDigimax(id, compressedFileSourcePath);

                await _repository.Create(presuncion);
                await _eventBus.Publish(presuncion.PullDomainEvents());
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                _eventBus.Publish(new ImportPresuncionDigimaxFailedEvent(compressedFileSourcePath));
            }

        }
    }
}
