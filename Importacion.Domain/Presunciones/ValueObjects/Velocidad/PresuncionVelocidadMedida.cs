using SharedKernel.Domain.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects.Velocidad;

public sealed record PresuncionVelocidadMedida : FloatValueObject
{
    private const float MinValue = 40f;
    private const float MaxValue = 300f;

    public PresuncionVelocidadMedida(float value, float velocidadMaxima) : base(value)
    {
        EnsureMinValue(value);
        EnsureMaxValue(value);
        EnsureGreaterThanMaximumSpeed(value, velocidadMaxima);
    }

    private static void EnsureMinValue(float value)
    {
        if (value < MinValue)
        {
            throw new InvalidPresuncionVelocidadMedidaException($"La velocidad medida debe ser mayor o igual a {MinValue}");
        }
    }

    private static void EnsureMaxValue(float value)
    {
        if (value > MaxValue)
        {
            throw new InvalidPresuncionVelocidadMedidaException($"La velocidad medida debe ser menor o igual a {MaxValue}");
        }
    }

    private static void EnsureGreaterThanMaximumSpeed(float value, float velocidadMaxima)
    {
        if (value <= velocidadMaxima)
        {
            throw new InvalidPresuncionVelocidadMedidaException($"La velocidad medida ({value}) debe ser mayor que la velocidad máxima ({velocidadMaxima})");
        }
    }
}