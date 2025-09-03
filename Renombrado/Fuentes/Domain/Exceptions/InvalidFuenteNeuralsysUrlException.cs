using SharedKernel.Domain.Exceptions;

namespace Renombrado.Fuentes.Domain.Exceptions;

public sealed class InvalidFuenteNeuralsysUrlException : DomainException
{
    public InvalidFuenteNeuralsysUrlException(string message) : base(message)
    {
    }
}