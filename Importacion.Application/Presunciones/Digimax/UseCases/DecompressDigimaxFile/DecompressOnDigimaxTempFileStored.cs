using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionDigimaxTempCompressedFileStoredEvent.
    /// Este handler inicia el proceso de descompresión de archivos temporales comprimidos
    /// cuando se detecta que un archivo comprimido de Digimax ha sido almacenado temporalmente.
    /// </summary>
    public class DecompressOnDigimaxTempFileStored(DecompressDigimaxFileUseCase useCase) : IConsumer<DigimaxTempFileStoredEvent>
    {
        private readonly DecompressDigimaxFileUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de archivo comprimido temporal almacenado.
        /// Ejecuta el caso de uso de descompresión con la ruta del archivo comprimido.
        /// </summary>
        /// <param name="context">Contexto del evento con la ruta del archivo comprimido.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<DigimaxTempFileStoredEvent> context)
        {
            await _useCase.Execute(context.Message.Path, context.Message.OriginalSourcePath);
        }
    }
}
