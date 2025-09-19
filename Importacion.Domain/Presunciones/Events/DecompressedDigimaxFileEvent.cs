using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events;

public record DecompressedDigimaxFileEvent(string BasePath, string PresuncionId) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.presuncion_digimax_decompressed_temp_file";
}