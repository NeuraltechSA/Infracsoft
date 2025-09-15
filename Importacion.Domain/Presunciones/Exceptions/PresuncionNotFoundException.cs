using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class PresuncionNotFoundException : DomainException
{
    public PresuncionNotFoundException(string message) : base(message)
    {
    }
}