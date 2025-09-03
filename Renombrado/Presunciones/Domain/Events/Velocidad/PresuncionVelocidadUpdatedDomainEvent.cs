using SharedKernel.Domain.Contracts;

namespace Renombrado.Presunciones.Domain.Events.Velocidad;

public sealed record PresuncionVelocidadUpdatedDomainEvent(
) : DomainEvent
{
    public required string PresuncionId { get; init; }
    public required DateTime? FechaHora { get; init; }
    public required string Lugar { get; init; }
    public required string Patente { get; init; }
    public required float VelocidadMedida { get; init; }
    public required float VelocidadMaxima { get; init; }
    public required int Carril { get; init; }

    public override string MessageName => "infracsoft.renombrado.presunciones.velocidad.updated";
}