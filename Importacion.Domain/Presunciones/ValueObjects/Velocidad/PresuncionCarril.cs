using SharedKernel.Domain.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects.Velocidad;

public sealed record PresuncionCarril : IntValueObject
{
    private const int MinValue = 1;
    private const int MaxValue = 5;

    public PresuncionCarril(int value) : base(value)
    {
        EnsureMinValue(value);
        EnsureMaxValue(value);
    }

    private static void EnsureMinValue(int value)
    {
        if (value < MinValue)
        {
            throw new InvalidPresuncionCarrilException($"El carril debe ser mayor o igual a {MinValue}");
        }
    }

    private static void EnsureMaxValue(int value)
    {
        if (value > MaxValue)
        {
            throw new InvalidPresuncionCarrilException($"El carril debe ser menor o igual a {MaxValue}");
        }
    }
}