using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;


namespace Infracsoft.Importacion.Application.Presunciones.UseCases.StoreImportedPresuncionFiles
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionImportedEvent.
    /// Este handler inicia el proceso de almacenamiento permanente de archivos
    /// cuando una presunción ha sido exitosamente importada a la base de datos.
    /// </summary>
    public class StoreImportedPresuncionFilesOnPresuncionImported(
        StoreImportedPresuncionFilesUseCase useCase) : IConsumer<PresuncionImportedEvent>
    {
        private readonly StoreImportedPresuncionFilesUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de presunción importada.
        /// Ejecuta el caso de uso de almacenamiento permanente con los datos del evento.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos de la presunción importada.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionImportedEvent> context)
        {
            await _useCase.Execute(
                context.Message.PresuncionSourcePath,
                context.Message.PresuncionDestinationPath,
                context.Message.PresuncionId
            );
        }
    }
}