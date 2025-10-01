using System;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;

public sealed record UnidadProduccionNombre : StringValueObject
{
    private const int MinLength = 1;
    private const int MaxLength = 255;

    public UnidadProduccionNombre(string value) : base(value)
    {
        EnsureMinLength(value);
        EnsureMaxLength(value);
        EnsureValidFormat(value);
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new InvalidUnidadProduccionNombreException($"El nombre de la unidad de producción debe tener al menos {MinLength} caracter");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidUnidadProduccionNombreException($"El nombre de la unidad de producción no puede exceder {MaxLength} caracteres");
        }
    }

    private static void EnsureValidFormat(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidUnidadProduccionNombreException("El nombre de la unidad de producción no puede estar vacío");
        }
    }
}
