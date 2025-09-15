using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionPatenteException : DomainException
{
    public InvalidPresuncionPatenteException(string message) : base(message)
    {
    }
}