using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;

/// <summary>
/// Excepción que se lanza cuando el nombre de unidad de producción no es válido.
/// </summary>
public sealed class InvalidUnidadProduccionNombreException : DomainException
{
    public InvalidUnidadProduccionNombreException(string message) : base(message) { }
    
    public static InvalidUnidadProduccionNombreException CreateForMinLength(int minLength)
    {
        return new InvalidUnidadProduccionNombreException(
            $"El nombre de la unidad de producción debe tener al menos {minLength} caracter");
    }
    
    public static InvalidUnidadProduccionNombreException CreateForMaxLength(int maxLength)
    {
        return new InvalidUnidadProduccionNombreException(
            $"El nombre de la unidad de producción no puede exceder {maxLength} caracteres");
    }
    
    public static InvalidUnidadProduccionNombreException CreateForEmptyValue()
    {
        return new InvalidUnidadProduccionNombreException(
            "El nombre de la unidad de producción no puede estar vacío");
    }
}
