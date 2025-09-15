using Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles
{
    /// <summary>
    /// Event handler de compensación que reacciona al evento PresuncionImportFailedEvent.
    /// Este handler elimina los archivos temporales cuando falla la importación de una presunción,
    /// activando la limpieza automática como parte del mecanismo de compensación.
    /// </summary>
    public class DeletePresuncionTempFilesOnPresuncionImportFailed(DeletePresuncionTempFilesUseCase useCase) 
        : IConsumer<PresuncionImportFailedEvent>
    {
        private readonly DeletePresuncionTempFilesUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de fallo en la importación de presunción.
        /// Ejecuta el caso de uso de eliminación de archivos temporales para limpiar
        /// los archivos que no pudieron ser procesados exitosamente.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos del fallo de importación.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionImportFailedEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionTempStoreKey);
        }
    }
}
