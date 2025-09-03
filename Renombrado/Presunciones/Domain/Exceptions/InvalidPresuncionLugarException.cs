using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionLugarException : DomainException
{
    public InvalidPresuncionLugarException(string message) : base(message)
    {
    }
}