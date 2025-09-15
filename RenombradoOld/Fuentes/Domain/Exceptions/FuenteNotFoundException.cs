using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.Exceptions;

public sealed class FuenteNotFoundException : DomainException
{
    public FuenteNotFoundException(string id) : base($"Fuente no encontrada: {id}")
    {
    }
}