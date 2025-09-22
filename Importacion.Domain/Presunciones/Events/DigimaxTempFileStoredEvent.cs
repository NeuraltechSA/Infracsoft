using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record DigimaxTempFileStoredEvent(string Path,string OriginalSourcePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.digimax_temp_file_stored";
}