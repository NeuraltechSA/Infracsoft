using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;

/// <summary>
/// Excepción que se lanza cuando se intenta crear una unidad de producción con un nombre que ya existe.
/// </summary>
public sealed class UnidadProduccionNombreAlreadyExistsException : DomainException
{
    public UnidadProduccionNombreAlreadyExistsException(string message) : base(message) { }
    
    public static UnidadProduccionNombreAlreadyExistsException Create(string nombre)
    {
        return new UnidadProduccionNombreAlreadyExistsException(
            $"Ya existe una unidad de producción con el nombre '{nombre}'. El nombre de la unidad de producción debe ser único.");
    }
}
