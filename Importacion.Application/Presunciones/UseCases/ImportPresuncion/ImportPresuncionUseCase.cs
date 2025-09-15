using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.ImportPresuncion
{
    /// <summary>
    /// Caso de uso para importar una presunción desde archivos temporales.
    /// Este caso de uso procesa los datos raw de una presunción almacenados temporalmente,
    /// los parsea, crea la entidad de dominio y la persiste en la base de datos.
    /// Incluye manejo de errores con eventos de compensación.
    /// </summary>
    public class ImportPresuncionUseCase(
        IPresuncionTempStore fileStore,
        IPresuncionParser parser,
        IGuidGenerator guidGenerator,
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    )
    {
        private readonly IPresuncionTempStore _fileStore = fileStore;
        private readonly IPresuncionParser _parser = parser;
        private readonly IGuidGenerator _guidGenerator = guidGenerator;
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEventBus _eventBus = eventBus;

        /// <summary>
        /// Ejecuta la importación de una presunción desde archivos temporales.
        /// Obtiene los datos raw, genera un ID único, parsea la presunción,
        /// la persiste en la base de datos y publica el evento de importación exitosa.
        /// En caso de error, publica un evento de fallo para activar la compensación.
        /// </summary>
        /// <param name="presuncionTempStoreKey">Clave del almacenamiento temporal donde están los archivos de la presunción.</param>
        /// <param name="presuncionSourcePath">Ruta original de la presunción en la fuente.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        /// <exception cref="Exception">Se relanza cualquier excepción después de publicar el evento de fallo.</exception>
        public async Task Execute(string presuncionTempStoreKey, string presuncionSourcePath)
        {
            string? presuncionId = null;
            try
            {
                var rawPresuncion = await _fileStore.GetRawPresuncionData(presuncionTempStoreKey);
                presuncionId = _guidGenerator.GenerateGuid().ToString();
                var presuncion = await _parser.ParsePresuncion(presuncionId, rawPresuncion);

                await _repository.Create(presuncion);
                await _unitOfWork.SaveChangesAsync();
                await _eventBus.Publish(new PresuncionImportedEvent(
                    presuncionId,
                    presuncionSourcePath,
                    presuncionTempStoreKey
                ));
            }
            catch (Exception ex)
            {
                await _eventBus.Publish(new PresuncionImportFailedEvent(
                    presuncionId,
                    presuncionSourcePath,
                    presuncionTempStoreKey,
                    ex.Message,
                    ex
                ));
                throw;
            }
        }
    }
}
