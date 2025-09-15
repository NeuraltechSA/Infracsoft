using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando las imágenes de una presunción han sido almacenadas exitosamente.
    /// </summary>
    public record PresuncionImagesStoredEvent(
        string PresuncionId,
        string RemotePath,
        int ImagesCount
    ) : DomainEvent
    {
        public override string MessageName => "infracsoft.importacion.presuncion.images.stored";
    }
}

