using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record PresuncionDigimaxImagesStoredEvent(string PresuncionId, string CompressedFileSourcePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.digimax_images_stored";
}
