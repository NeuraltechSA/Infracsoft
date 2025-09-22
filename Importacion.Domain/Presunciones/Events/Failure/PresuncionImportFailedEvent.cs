using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events.Failure;

/// <summary>
/// Evento de dominio que se publica cuando falla la importación de una presunción.
/// Este evento activa el mecanismo de compensación para limpiar archivos temporales
/// y eliminar imágenes subidas.
/// </summary>
public record PresuncionImportFailedEvent(
    string? PresuncionId,
    string PresuncionSourcePath
) : DomainEvent
{
    public override string MessageName => "infracsoft.importacion.presunciones.presuncion_import_failed";
}