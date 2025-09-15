using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionFechaHoraException : DomainException
{
    public InvalidPresuncionFechaHoraException(string message) : base(message)
    {
    }
}