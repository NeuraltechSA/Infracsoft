using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionPatenteException : DomainException
{
    public InvalidPresuncionPatenteException(string message) : base(message)
    {
    }
}