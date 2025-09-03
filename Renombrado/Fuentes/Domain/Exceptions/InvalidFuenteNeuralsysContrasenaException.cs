using SharedKernel.Domain.Exceptions;

namespace Renombrado.Fuentes.Domain.Exceptions;

public sealed class InvalidFuenteNeuralsysContrasenaException : DomainException
{
    public InvalidFuenteNeuralsysContrasenaException(string message) : base(message)
    {
    }
}