using SharedKernel.Domain.ValueObjects;
using RenombradoOld.Fuentes.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.ValueObjects.Neuralsys;

public sealed record FuenteNeuralsysUsuario : StringValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 50;

    public FuenteNeuralsysUsuario(string value) : base(value)
    {
        EnsureNotEmpty(value);
        EnsureMinLength(value);
        EnsureMaxLength(value);
    }

    private static void EnsureNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidFuenteNeuralsysUsuarioException("El usuario de Neuralsys no puede estar vacío");
        }
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new InvalidFuenteNeuralsysUsuarioException($"El usuario de Neuralsys debe tener al menos {MinLength} caracteres");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidFuenteNeuralsysUsuarioException($"El usuario de Neuralsys no puede tener más de {MaxLength} caracteres");
        }
    }
}