using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.ParsePresuncionFromFile
{
    public class StorePresuncionTempFilesUseCase(
        IPresuncionSource fileSource,
        IPresuncionTempStore fileStore
    )
    {
        private readonly IPresuncionSource _fileSource = fileSource;
        private readonly IPresuncionTempStore _fileStore = fileStore;

        public async Task Execute(string presuncionSourcePath)
        {

            await foreach (var file in _fileSource.GetPresuncionFiles(presuncionSourcePath))
            {
                using var stream = file;
                // Utilizo la ruta de la presuncion como clave unica temporal
                await _fileStore.Store(presuncionSourcePath, stream);
            }


            /*
            //1. Download presuncion files
           await foreach (var file in _fileSource.GetPresuncionFiles(presuncionPath))
           {
                using var stream = file;
                await _fileStore.Store(stream, presuncionPath);
           }
           
            //2. Process presuncion
            var rawPresuncion = await _fileStore.GetRawPresuncionData(presuncionPath);
            var presuncion = await _parser.ParsePresuncion(_guidGenerator.GenerateGuid().ToString(), rawPresuncion);

            //3. Persist and notify
            presuncion.Import(presuncionPath);
            await _repository.Create(presuncion);
            await _unitOfWork.SaveChangesAsync();
            await _eventBus.Publish(presuncion.PullDomainEvents());*/
        }

    }
}
