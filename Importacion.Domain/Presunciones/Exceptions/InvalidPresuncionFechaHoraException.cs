using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionFechaHoraException : DomainException
{
    public InvalidPresuncionFechaHoraException(string message) : base(message)
    {
    }
}