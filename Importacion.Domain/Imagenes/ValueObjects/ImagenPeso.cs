using System;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Imagenes.ValueObjects;

public sealed record ImagenPeso : FloatValueObject
{
    private const float MinValue = 0.0f;
    private const float MaxValue = 100.0f; // 100 MB máximo

    public ImagenPeso(float value) : base(value)
    {
        //EnsureValidRange(value);TODO:
    }

    private static void EnsureValidRange(float value)
    {
        if (value < MinValue)
        {
            throw new ArgumentException($"El peso de la imagen no puede ser menor a {MinValue} MB");
        }

        if (value > MaxValue)
        {
            throw new ArgumentException($"El peso de la imagen no puede ser mayor a {MaxValue} MB");
        }
    }
}