using SharedKernel.Domain.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects.Velocidad;

public sealed record PresuncionVelocidadMaxima : FloatValueObject
{
    private const float MinValue = 40f;
    private const float MaxValue = 130f;

    public PresuncionVelocidadMaxima(float value) : base(value)
    {
        EnsureMinValue(value);
        EnsureMaxValue(value);
    }

    private static void EnsureMinValue(float value)
    {
        if (value < MinValue)
        {
            throw new InvalidPresuncionVelocidadMaximaException($"La velocidad máxima debe ser mayor o igual a {MinValue}");
        }
    }

    private static void EnsureMaxValue(float value)
    {
        if (value > MaxValue)
        {
            throw new InvalidPresuncionVelocidadMaximaException($"La velocidad máxima debe ser menor o igual a {MaxValue}");
        }
    }
}