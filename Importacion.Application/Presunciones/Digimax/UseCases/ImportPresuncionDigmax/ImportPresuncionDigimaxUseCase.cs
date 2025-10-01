using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax
{
    public class ImportPresuncionDigimaxUseCase(
        IGuidGenerator guidGenerator,
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        IEquipoRepository equipoRepository
    )
    {
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;
        private readonly EquipoPathExtractor _equipoExtractor = new EquipoPathExtractor(equipoRepository);
        private readonly PresuncionDuplicateValidator _duplicateValidator = new PresuncionDuplicateValidator(repository);
        private readonly DigimaxDataExtractor _dataExtractor = new DigimaxDataExtractor();

        public async Task Execute(List<string> imagenesIds, string compressedFileSourcePath)
        {
            try
            {
                var presuncionId = _guidGenerator.GenerateGuid().ToString();
                
                // Extraer datos del archivo y equipo por separado
                var data = _dataExtractor.ExtractData(compressedFileSourcePath);
                var equipo = await _equipoExtractor.ExtractEquipoFromPath(compressedFileSourcePath);
                
                // Validar que no exista una presunción duplicada
                await _duplicateValidator.EnsureNoDuplicate(data.FechaHora, data.Lugar, equipo.Id.Value);
                
                // Crear presunción con datos extraídos
                var presuncion = PresuncionVelocidad.Create(
                    presuncionId,
                    equipo.Id.Value,
                    imagenesIds,
                    data.VelocidadMedida,
                    data.VelocidadMaxima,
                    null, // Carril no disponible en Digimax
                    data.FechaHora,
                    data.Lugar,
                    null
                );

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
