using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record DigimaxImagesStoredEvent(
    IEnumerable<string> ImageIds,
    string CompressedFileSourcePath,
    string CompressedFileTempPath,
    string TempBasePath
) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.digimax_images_stored";
}
