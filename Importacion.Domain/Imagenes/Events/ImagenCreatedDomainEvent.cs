using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Imagenes.Events;

public sealed record ImagenCreatedDomainEvent(

) : DomainEvent
{
    public required string ImagenId { get; init; }
    public required string Ruta { get; init; }
    public required float Peso { get; init; }
    public required string Nombre { get; init; }
    public string? PresuncionId { get; init; }

    public override string MessageName => "infracsoft.importacion.imagenes.created";
}