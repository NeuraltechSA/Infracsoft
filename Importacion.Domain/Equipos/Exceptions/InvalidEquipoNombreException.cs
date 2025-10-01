using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Equipos.Exceptions;

/// <summary>
/// Excepción que se lanza cuando el nombre del equipo no es válido.
/// </summary>
public sealed class InvalidEquipoNombreException : DomainException
{
    public InvalidEquipoNombreException(string message) : base(message) { }
    
    public static InvalidEquipoNombreException CreateForMinLength(int minLength)
    {
        return new InvalidEquipoNombreException(
            $"El nombre del equipo debe tener al menos {minLength} caracter");
    }
    
    public static InvalidEquipoNombreException CreateForMaxLength(int maxLength)
    {
        return new InvalidEquipoNombreException(
            $"El nombre del equipo no puede exceder {maxLength} caracteres");
    }
    
    public static InvalidEquipoNombreException CreateForEmptyValue()
    {
        return new InvalidEquipoNombreException(
            "El nombre del equipo no puede estar vacío");
    }
}
