using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionLugarException : DomainException
{
    public InvalidPresuncionLugarException(string message) : base(message)
    {
    }
}