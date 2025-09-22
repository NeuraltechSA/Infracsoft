using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events.Failure;

public record DigimaxDecompressionFailedEvent(string TempFilePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.digimax_decompression_failed";
}

