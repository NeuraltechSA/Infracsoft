using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Equipos.Exceptions;

/// <summary>
/// Excepción que se lanza cuando un equipo no existe.
/// </summary>
public sealed class EquipoNotExistsException : DomainException
{
    public EquipoNotExistsException(string message) : base(message) { }
    
    public static EquipoNotExistsException CreateFromId(string id)
    {
        return new EquipoNotExistsException(
            $"El equipo con ID {id} no existe");
    }
    
    public static EquipoNotExistsException CreateFromNombre(string nombre)
    {
        return new EquipoNotExistsException(
            $"El equipo con nombre '{nombre}' no existe");
    }
}
