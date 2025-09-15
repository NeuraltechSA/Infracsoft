using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionLugarException : DomainException
{
    public InvalidPresuncionLugarException(string message) : base(message)
    {
    }
}