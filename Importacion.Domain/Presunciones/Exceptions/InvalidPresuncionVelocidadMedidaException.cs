using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionVelocidadMedidaException : DomainException
{
    public InvalidPresuncionVelocidadMedidaException(string message) : base(message)
    {
    }
}