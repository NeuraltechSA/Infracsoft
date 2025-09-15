using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionCarrilException : DomainException
{
    public InvalidPresuncionCarrilException(string message) : base(message)
    {
    }
}