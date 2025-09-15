using SharedKernel.Domain.ValueObjects;
using RenombradoOld.Fuentes.Domain.Exceptions;

namespace RenombradoOld.Fuentes.Domain.ValueObjects.Neuralsys;

public sealed record FuenteNeuralsysContrasena : StringValueObject
{
    private const int MinLength = 2;
    private const int MaxLength = 100;

    public FuenteNeuralsysContrasena(string value) : base(value)
    {
        EnsureMinLength(value);
        EnsureMaxLength(value);
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new InvalidFuenteNeuralsysContrasenaException($"La contraseña de Neuralsys debe tener al menos {MinLength} caracteres");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidFuenteNeuralsysContrasenaException($"La contraseña de Neuralsys no puede tener más de {MaxLength} caracteres");
        }
    }
}