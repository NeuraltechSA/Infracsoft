using System;
using System.Text.RegularExpressions;
using SharedKernel.Domain.ValueObjects;

namespace RenombradoOld.Fuentes.Domain.ValueObjects;

public sealed record FuenteNombre : StringValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 100;

    public FuenteNombre(string value) : base(value)
    {
        EnsureMinLength(value);
        EnsureMaxLength(value);
        EnsureValidFormat(value);
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"El nombre de la fuente no puede exceder {MaxLength} caracteres");
        }
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new ArgumentException($"El nombre de la fuente debe tener al menos {MinLength} caracteres");
        }
    }

    private static void EnsureValidFormat(string value)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s\-_]+$"))
        {
            throw new ArgumentException("El nombre de la fuente solo puede contener letras, números, espacios, guiones y guiones bajos");
        }
    }

}