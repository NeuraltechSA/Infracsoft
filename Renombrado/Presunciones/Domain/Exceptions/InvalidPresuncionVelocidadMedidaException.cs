using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionVelocidadMedidaException : DomainException
{
    public InvalidPresuncionVelocidadMedidaException(string message) : base(message)
    {
    }
}