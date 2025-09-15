using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.Exceptions;

public sealed class InvalidFuenteNeuralsysContrasenaException : DomainException
{
    public InvalidFuenteNeuralsysContrasenaException(string message) : base(message)
    {
    }
}