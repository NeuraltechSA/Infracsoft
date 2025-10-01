using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;

/// <summary>
/// Excepción que se lanza cuando una unidad de producción no existe.
/// </summary>
public sealed class UnidadProduccionNotExistsException : DomainException
{
    public UnidadProduccionNotExistsException(string message) : base(message) { }
    
    public static UnidadProduccionNotExistsException CreateFromId(string id)
    {
        return new UnidadProduccionNotExistsException(
            $"La unidad de producción con ID {id} no existe");
    }
    
    public static UnidadProduccionNotExistsException CreateFromNombre(string nombre)
    {
        return new UnidadProduccionNotExistsException(
            $"La unidad de producción con nombre '{nombre}' no existe");
    }
}
