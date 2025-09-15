using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando los archivos de una presunción han sido
    /// almacenados permanentemente. Este evento activa la limpieza de archivos temporales
    /// y la eliminación de la presunción de la fuente original.
    /// </summary>
    /// <param name="PresuncionId">ID único de la presunción procesada.</param>
    /// <param name="TempStoreKey">Clave del almacenamiento temporal que se debe limpiar.</param>
    /// <param name="SourcePath">Ruta de la presunción en la fuente que se debe eliminar.</param>
    public record PresuncionFilesStoredEvent(
        string PresuncionId,
        string TempStoreKey,
        string SourcePath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.files_stored";
    }
}

