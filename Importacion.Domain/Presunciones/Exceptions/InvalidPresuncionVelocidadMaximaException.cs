using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionVelocidadMaximaException : DomainException
{
    public InvalidPresuncionVelocidadMaximaException(string message) : base(message)
    {
    }
}