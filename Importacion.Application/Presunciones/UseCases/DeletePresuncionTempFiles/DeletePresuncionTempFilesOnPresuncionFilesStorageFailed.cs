using Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles
{
    /// <summary>
    /// Event handler de compensación que reacciona al evento PresuncionFilesStorageFailedEvent.
    /// Este handler elimina los archivos temporales cuando falla el almacenamiento permanente
    /// de archivos, completando la limpieza como parte del mecanismo de compensación.
    /// </summary>
    public class DeletePresuncionTempFilesOnPresuncionFilesStorageFailed(DeletePresuncionTempFilesUseCase useCase) 
        : IConsumer<PresuncionFilesStorageFailedEvent>
    {
        private readonly DeletePresuncionTempFilesUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de fallo en el almacenamiento de archivos de presunción.
        /// Ejecuta el caso de uso de eliminación de archivos temporales para limpiar
        /// los archivos que no pudieron ser almacenados permanentemente.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos del fallo de almacenamiento.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionFilesStorageFailedEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionTempStoreKey);
        }
    }
}
