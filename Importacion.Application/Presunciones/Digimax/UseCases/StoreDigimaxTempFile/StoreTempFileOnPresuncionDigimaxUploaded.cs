using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionDigimaxUploadedEvent.
    /// Este handler inicia el proceso de almacenamiento temporal del archivo comprimido
    /// cuando se detecta que un archivo de Digimax ha sido subido.
    /// </summary>
    public class StoreTempFileOnPresuncionDigimaxUploaded(StoreDigimaxTempFileUseCase useCase) : IConsumer<PresuncionDigimaxUploadedEvent>
    {
        private readonly StoreDigimaxTempFileUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de archivo Digimax subido.
        /// Ejecuta el caso de uso de almacenamiento temporal con la ruta del archivo comprimido.
        /// </summary>
        /// <param name="context">Contexto del evento con la ruta del archivo comprimido.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionDigimaxUploadedEvent> context)
        {
            await _useCase.Execute(context.Message.CompressedFileSourcePath);
        }
    }
}
