using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.Exceptions;

public sealed class FuenteAlreadyExistsException : DomainException
{
    public FuenteAlreadyExistsException(string id) : base($"Fuente con id {id} ya existe")
    {
    }
}
