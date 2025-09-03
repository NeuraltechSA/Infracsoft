using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class InvalidPresuncionVelocidadMaximaException : DomainException
{
    public InvalidPresuncionVelocidadMaximaException(string message) : base(message)
    {
    }
}