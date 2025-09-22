using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events.Failure
{
    /// <summary>
    /// Evento de dominio que se publica cuando falla el almacenamiento permanente de archivos
    /// de una presunción. Este evento activa el mecanismo de compensación para eliminar
    /// la presunción de la base de datos y limpiar los archivos temporales.
    /// </summary>
    /// <param name="PresuncionId">ID único de la presunción que falló en el almacenamiento.</param>
    /// <param name="PresuncionSourcePath">Ruta original de la presunción en la fuente.</param>
    /// <param name="PresuncionDestinationPath">Ruta de destino que se debe limpiar.</param>
    public record PresuncionFilesStorageFailedEvent(
        string PresuncionId,
        string PresuncionSourcePath,
        string PresuncionDestinationPath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.files_storage_failed";
    }
}
