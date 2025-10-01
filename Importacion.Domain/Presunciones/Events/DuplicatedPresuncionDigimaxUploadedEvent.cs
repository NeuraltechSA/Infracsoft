using SharedKernel.Domain.Contracts;
namespace Infracsoft.Importacion.Domain.Presunciones.Events;

/// <summary>
/// Evento que se dispara cuando se detecta una presunción Digimax duplicada.
/// </summary>
public sealed record DuplicatedPresuncionDigimaxUploadedEvent(
    string CompressedFileSourcePath
) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.duplicated-digimax-uploaded";
}
