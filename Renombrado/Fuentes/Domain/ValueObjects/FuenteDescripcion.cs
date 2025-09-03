using System;
using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Fuentes.Domain.ValueObjects;

public sealed record FuenteDescripcion : NullableStringValueObject
{
    private const int MAX_LENGTH = 500;

    public FuenteDescripcion(string? value) : base(value)
    {
        EnsureValidLength(value);
    }

    private void EnsureValidLength(string? value)
    {
        if (value != null && value.Length > MAX_LENGTH)
        {
            throw new ArgumentException($"La descripción no puede exceder {MAX_LENGTH} caracteres");
        }
    }

}