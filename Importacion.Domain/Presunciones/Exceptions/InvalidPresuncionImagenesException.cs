using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

public sealed class InvalidPresuncionImagenesException : DomainException
{
    public InvalidPresuncionImagenesException(string message) : base(message)
    {
    }

    public static InvalidPresuncionImagenesException CreateMinImages(int minImages, int actualCount)
    {
        return new InvalidPresuncionImagenesException($"Una presunción debe tener al menos {minImages} imagen. Se encontraron {actualCount} imágenes.");
    }

    public static InvalidPresuncionImagenesException CreateMaxImages(int maxImages, int actualCount)
    {
        return new InvalidPresuncionImagenesException($"Una presunción no puede tener más de {maxImages} imágenes. Se encontraron {actualCount} imágenes.");
    }
}
