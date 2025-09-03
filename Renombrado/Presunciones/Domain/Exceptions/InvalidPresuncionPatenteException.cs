using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionPatenteException : DomainException
{
    public InvalidPresuncionPatenteException(string message) : base(message)
    {
    }
}