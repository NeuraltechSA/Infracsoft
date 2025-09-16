using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.ImportPresuncion
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionTempFilesStoredEvent.
    /// Este handler inicia el proceso de importación de una presunción cuando
    /// sus archivos han sido almacenados temporalmente.
    /// </summary>
    public class ImportPresuncionOnPresuncionTempFilesStored(ImportPresuncionUseCase useCase) 
        : IConsumer<PresuncionTempFilesStoredEvent>
    {
        private readonly ImportPresuncionUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de archivos temporales almacenados.
        /// Ejecuta el caso de uso de importación con los datos del evento.
        /// </summary>
        /// <param name="context">Contexto del evento con los datos de la presunción.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionTempFilesStoredEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionDestinationPath, context.Message.PresuncionSourcePath);
        }
    }
}
