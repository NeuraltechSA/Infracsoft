using SharedKernel.Domain.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

public sealed record PresuncionLugar : StringValueObject
{
    private const int MaxLength = 200;

    public PresuncionLugar(string value) : base(value)
    {
        EnsureNotEmpty(value);
        EnsureMaxLength(value);
    }

    private static void EnsureNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPresuncionLugarException("El lugar no puede estar vacío");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidPresuncionLugarException($"El lugar no puede tener más de {MaxLength} caracteres");
        }
    }
}