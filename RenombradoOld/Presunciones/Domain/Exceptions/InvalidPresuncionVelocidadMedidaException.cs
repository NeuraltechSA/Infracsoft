using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionVelocidadMedidaException : DomainException
{
    public InvalidPresuncionVelocidadMedidaException(string message) : base(message)
    {
    }
}