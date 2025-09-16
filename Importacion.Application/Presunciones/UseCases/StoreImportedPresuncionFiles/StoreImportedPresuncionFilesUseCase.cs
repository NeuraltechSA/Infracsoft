using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.StoreImportedPresuncionFiles
{
    /// <summary>
    /// Caso de uso para almacenar archivos de presunción importada en el almacenamiento permanente.
    /// Este caso de uso procesa las imágenes de una presunción ya importada y las almacena
    /// en el sistema de archivos permanente, activando posteriormente la limpieza de archivos temporales.
    /// </summary>
    public class StoreImportedPresuncionFilesUseCase(
        PresuncionFileStore fileStore,
        IPresuncionTempStore tempFileStore,
        IEventBus eventBus
    )
    {
        private readonly PresuncionFileStore _fileStore = fileStore;
        private readonly IPresuncionTempStore _tempFileStore = tempFileStore;
        private readonly IEventBus _eventBus = eventBus;

        /// <summary>
        /// Ejecuta el almacenamiento permanente de archivos de presunción importada.
        /// Obtiene las imágenes desde el almacenamiento temporal, las almacena permanentemente
        /// y publica el evento de almacenamiento exitoso. En caso de error, publica un evento
        /// de fallo para activar la compensación.
        /// </summary>
        /// <param name="sourcePath">Ruta original de la presunción en la fuente.</param>
        /// <param name="presuncionDestinationPath">Ruta de destino donde están los archivos temporales.</param>
        /// <param name="presuncionId">ID único de la presunción importada.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        /// <exception cref="Exception">Se relanza cualquier excepción después de publicar el evento de fallo.</exception>
        public async Task Execute(string sourcePath, string presuncionDestinationPath, string presuncionId)
        {
            try
            {
                var images = _tempFileStore.GetPresuncionImages(presuncionDestinationPath);
                await _fileStore.StoreImages(images, presuncionId);

                await _eventBus.Publish(new PresuncionFilesStoredEvent(
                    presuncionId, 
                    presuncionDestinationPath, 
                    sourcePath)
                );
            }
            catch (Exception ex)
            {
                await _eventBus.Publish(new PresuncionFilesStorageFailedEvent(
                    presuncionId,
                    sourcePath,
                    presuncionDestinationPath,
                    ex.Message,
                    ex
                ));
                throw;
            }
        }
    }
}
