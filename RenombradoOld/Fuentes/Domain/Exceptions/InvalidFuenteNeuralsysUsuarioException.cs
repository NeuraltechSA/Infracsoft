using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.Exceptions;

public sealed class InvalidFuenteNeuralsysUsuarioException : DomainException
{
    public InvalidFuenteNeuralsysUsuarioException(string message) : base(message)
    {
    }
}