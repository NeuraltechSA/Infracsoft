using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record DecompressedDigimaxFileEvent(string TempCompressedFilePath, string TempBasePath, string CompressedFileSourcePath) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.presuncion_digimax_decompressed_temp_file";
}