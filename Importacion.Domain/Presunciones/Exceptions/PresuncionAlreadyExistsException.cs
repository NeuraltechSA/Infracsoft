using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

/// <summary>
/// Excepción que se lanza cuando se intenta crear una presunción que ya existe.
/// Una presunción se considera duplicada si existe otra con la misma fecha/hora, lugar y equipo.
/// </summary>
public sealed class PresuncionAlreadyExistsException : DomainException
{
    public PresuncionAlreadyExistsException(string message) : base(message) { }
    
    public static PresuncionAlreadyExistsException Create(DateTime fechaHora, string lugar, string equipoId)
    {
        return new PresuncionAlreadyExistsException(
            $"Ya existe una presunción para el equipo {equipoId} en {lugar} el {fechaHora:dd/MM/yyyy HH:mm:ss}");
    }
}