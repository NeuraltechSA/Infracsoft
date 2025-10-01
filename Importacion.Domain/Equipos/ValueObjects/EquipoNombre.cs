using System;
using Infracsoft.Importacion.Domain.Equipos.Exceptions;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Equipos.ValueObjects;

public sealed record EquipoNombre : StringValueObject
{
    private const int MinLength = 1;
    private const int MaxLength = 255;

    public EquipoNombre(string value) : base(value)
    {
        EnsureMinLength(value);
        EnsureMaxLength(value);
        EnsureValidFormat(value);
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new InvalidEquipoNombreException($"El nombre del equipo debe tener al menos {MinLength} caracter");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidEquipoNombreException($"El nombre del equipo no puede exceder {MaxLength} caracteres");
        }
    }

    private static void EnsureValidFormat(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEquipoNombreException("El nombre del equipo no puede estar vacío");
        }
    }
}
