using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class PresuncionAlreadyExistsException : DomainException
{
    public PresuncionAlreadyExistsException(string message) : base(message)
    {
    }
}