using SharedKernel.Domain.Exceptions;

namespace RenombradoOld.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionVelocidadMaximaException : DomainException
{
    public InvalidPresuncionVelocidadMaximaException(string message) : base(message)
    {
    }
}