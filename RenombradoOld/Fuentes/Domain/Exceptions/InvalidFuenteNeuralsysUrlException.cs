using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.Exceptions;

public sealed class InvalidFuenteNeuralsysUrlException : DomainException
{
    public InvalidFuenteNeuralsysUrlException(string message) : base(message)
    {
    }
}