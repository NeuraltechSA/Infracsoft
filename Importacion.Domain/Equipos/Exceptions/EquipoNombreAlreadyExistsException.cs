using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Equipos.Exceptions;

/// <summary>
/// Excepción que se lanza cuando se intenta crear un equipo con un nombre que ya existe.
/// </summary>
public sealed class EquipoNombreAlreadyExistsException : DomainException
{
    public EquipoNombreAlreadyExistsException(string message) : base(message) { }
    
    public static EquipoNombreAlreadyExistsException Create(string nombre)
    {
        return new EquipoNombreAlreadyExistsException(
            $"Ya existe un equipo con el nombre '{nombre}'. El nombre del equipo debe ser único.");
    }
}
