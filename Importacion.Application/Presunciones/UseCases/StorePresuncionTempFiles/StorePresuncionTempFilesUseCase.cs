using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.ParsePresuncionFromFile
{
    /// <summary>
    /// Caso de uso para almacenar archivos de presunción en almacenamiento temporal.
    /// Este caso de uso descarga todos los archivos de una presunción desde la fuente
    /// y los almacena temporalmente para su posterior procesamiento.
    /// </summary>
    public class StorePresuncionTempFilesUseCase(
        IPresuncionSource fileSource,
        IPresuncionTempStore fileStore,
        IEventBus eventBus
    )
    {
        private readonly IPresuncionSource _fileSource = fileSource;
        private readonly IPresuncionTempStore _fileStore = fileStore;
        private readonly IEventBus _eventBus = eventBus;

        /// <summary>
        /// Ejecuta el almacenamiento temporal de archivos de presunción.
        /// Descarga todos los archivos de la presunción desde la fuente y los almacena
        /// temporalmente usando la ruta de la presunción como ruta de destino.
        /// </summary>
        /// <param name="presuncionSourcePath">Ruta de la presunción en la fuente.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Execute(string presuncionSourcePath)
        {
            // Utilizo la ruta de la presuncion como ruta de destino del almacenamiento temporal
            var presuncionDestinationPath = presuncionSourcePath;
            await foreach (var file in _fileSource.GetPresuncionFiles(presuncionSourcePath))
            {
                using var stream = file;
                await _fileStore.Store(presuncionDestinationPath, stream);
            }

            await _eventBus.Publish(new PresuncionTempFilesStoredEvent(presuncionSourcePath, presuncionDestinationPath));
        }
    }
}
