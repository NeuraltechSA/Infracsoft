using Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncion;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncion
{
    /// <summary>
    /// Event handler de compensación que reacciona al evento PresuncionFilesStorageFailedEvent.
    /// Este handler elimina la presunción de la base de datos cuando falla el almacenamiento
    /// de archivos, activando la compensación para mantener la consistencia del sistema.
    /// </summary>
    public class DeletePresuncionOnPresuncionFilesStorageFailed(DeletePresuncionUseCase useCase) 
        : IConsumer<PresuncionFilesStorageFailedEvent>
    {
        private readonly DeletePresuncionUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de fallo en el almacenamiento de archivos de presunción.
        /// Ejecuta el caso de uso de eliminación de presunción para revertir la importación
        /// que no pudo completarse exitosamente.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos del fallo de almacenamiento.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionFilesStorageFailedEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionId);
        }
    }
}
