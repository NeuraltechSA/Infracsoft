
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Events;

public sealed record UnidadProduccionDeletedDomainEvent() : DomainEvent
{
    public required string UnidadProduccionId { get; init; }
    public required string Nombre { get; init; }

    public override string MessageName => "infracsoft.importacion.unidadesproduccion.deleted";
}
