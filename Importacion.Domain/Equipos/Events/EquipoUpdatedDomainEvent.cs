using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Equipos.Events;

public sealed record EquipoUpdatedDomainEvent() : DomainEvent
{
    public required string EquipoId { get; init; }
    public required string Nombre { get; init; }
    public required string UnidadProduccionId { get; init; }

    public override string MessageName => "infracsoft.importacion.equipos.updated";
}
