using SharedKernel.Domain.Contracts;

namespace Renombrado.Fuentes.Domain.Events.Neuralsys;

public sealed record FuenteNeuralsysCreatedDomainEvent(
) : DomainEvent
{
    public required string FuenteId { get; init; }
    public required string Nombre { get; init; }
    public required string? Descripcion { get; init; }
    public required string Url { get; init; }
    public required string Usuario { get; init; }
    public required string Contrasena { get; init; }

    public override string MessageName => "infracsoft.renombrado.fuentes.neuralsys.created";
}