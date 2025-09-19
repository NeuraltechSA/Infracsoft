using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;
using System.Text.RegularExpressions;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.CheckSourceDigimax
{
    public class CheckSourceDigimaxUseCase(
        IPresuncionFileSource presuncionFileSource, 
        IEventBus eventBus,
        IUnitOfWork unitOfWork
    )
    {
        private readonly IPresuncionFileSource _presuncionFileSource = presuncionFileSource;
        private readonly IEventBus _eventBus = eventBus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute()
        {
            var filePaths = await _presuncionFileSource.GetAllFilePathsRecursive();
            var digimaxZipFiles = filePaths.Where(p => Regex.IsMatch(Path.GetFileName(p), PresuncionVelocidad.DigimaxFilenameRegex));

            foreach (var path in digimaxZipFiles)
            {
                await _eventBus.Publish(new PresuncionDigimaxUploadedEvent(path));
            }
            // Outbox the events
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
