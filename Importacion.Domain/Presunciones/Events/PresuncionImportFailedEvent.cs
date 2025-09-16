using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando falla la importación de una presunción.
    /// Este evento activa el mecanismo de compensación para limpiar los archivos temporales
    /// y mantener la consistencia del sistema.
    /// </summary>
    /// <param name="PresuncionId">ID único de la presunción (puede ser null si el fallo ocurrió antes de la generación del ID).</param>
    /// <param name="PresuncionSourcePath">Ruta original de la presunción en la fuente.</param>
    /// <param name="PresuncionDestinationPath">Ruta de destino que se debe limpiar.</param>
    /// <param name="ErrorMessage">Mensaje descriptivo del error que causó el fallo.</param>
    /// <param name="Exception">Excepción que causó el fallo en la importación.</param>
    public record PresuncionImportFailedEvent(
        string? PresuncionId,
        string PresuncionSourcePath,
        string PresuncionDestinationPath,
        string ErrorMessage,
        Exception Exception
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.import_failed";
    }
}
