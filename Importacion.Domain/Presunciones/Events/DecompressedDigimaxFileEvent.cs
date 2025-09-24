using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record DecompressedDigimaxFileEvent(string TempFilePath, string TempBasePath, string OriginalSourcePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.presuncion_digimax_decompressed_temp_file";
}