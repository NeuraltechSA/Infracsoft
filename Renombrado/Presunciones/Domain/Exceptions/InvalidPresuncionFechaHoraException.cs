using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionFechaHoraException : DomainException
{
    public InvalidPresuncionFechaHoraException(string message) : base(message)
    {
    }
}