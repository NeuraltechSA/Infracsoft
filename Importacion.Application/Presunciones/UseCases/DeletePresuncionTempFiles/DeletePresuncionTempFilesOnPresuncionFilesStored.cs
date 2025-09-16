using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionFilesStoredEvent.
    /// Este handler elimina los archivos temporales cuando todos los archivos
    /// han sido almacenados permanentemente, liberando espacio en el almacenamiento temporal.
    /// </summary>
    public class DeletePresuncionTempFilesOnPresuncionFilesStored(DeletePresuncionTempFilesUseCase useCase) 
        : IConsumer<PresuncionFilesStoredEvent>
    {
        private readonly DeletePresuncionTempFilesUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de archivos almacenados permanentemente.
        /// Ejecuta el caso de uso de eliminación de archivos temporales con la ruta de destino.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos de la presunción.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionFilesStoredEvent> context)
        {
            await _useCase.Execute(context.Message.DestinationPath);
        }
    }
}
