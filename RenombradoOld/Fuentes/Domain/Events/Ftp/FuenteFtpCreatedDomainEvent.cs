using SharedKernel.Domain.Contracts;

namespace RenombradoOld.Fuentes.Domain.Events;

public sealed record FuenteFtpCreatedDomainEvent(

) : DomainEvent
{

    public required string FuenteId { get; init; }
    public required string Nombre { get; init; }
    public required string? Descripcion { get; init; }
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }

    public override string MessageName => "infracsoft.renombrado.fuentes.ftp.created";
}
