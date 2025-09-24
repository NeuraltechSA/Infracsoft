using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events.Failure;

public record DigimaxImagesStorageFailedEvent(string TempBasePath, string TempFilePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.digimax_images_storage_failed";
}

