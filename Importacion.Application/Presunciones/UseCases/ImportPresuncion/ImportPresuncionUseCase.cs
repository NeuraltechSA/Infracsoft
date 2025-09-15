using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.ImportPresuncion
{
    public class ImportPresuncionUseCase(
        IPresuncionTempStore fileStore,
        IPresuncionParser parser,
        IGuidGenerator guidGenerator,
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    )
    {
        private readonly IPresuncionTempStore _fileStore = fileStore;
        private readonly IPresuncionParser _parser = parser;
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;
        public async Task Execute(string presuncionSourcePath)
        {
            var rawPresuncion = await _fileStore.GetRawPresuncionData(presuncionSourcePath);
            var presuncionId = _guidGenerator.GenerateGuid().ToString();
            var presuncion = await _parser.ParsePresuncion(presuncionId, rawPresuncion);
            presuncion.Import(presuncionId, presuncionSourcePath, presuncionSourcePath);

            await _repository.Create(presuncion);
            await _unitOfWork.SaveChangesAsync();
            await _eventBus.Publish(presuncion.PullDomainEvents());
        }
    }
}
