using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionCarrilException : DomainException
{
    public InvalidPresuncionCarrilException(string message) : base(message)
    {
    }
}