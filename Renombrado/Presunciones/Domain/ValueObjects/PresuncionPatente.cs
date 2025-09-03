using SharedKernel.Domain.ValueObjects;
using Renombrado.Presunciones.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Renombrado.Presunciones.Domain.ValueObjects;

public sealed record PresuncionPatente : StringValueObject
{
    private const int MaxLength = 8;
    private static readonly Regex PatenteRegex = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);

    public PresuncionPatente(string value) : base(value)
    {
        EnsureNotEmpty(value);
        EnsureMaxLength(value);
        EnsureValidFormat(value);
    }

    private static void EnsureNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPresuncionPatenteException("La patente no puede estar vacía");
        }
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidPresuncionPatenteException($"La patente no puede tener más de {MaxLength} caracteres");
        }
    }

    private static void EnsureValidFormat(string value)
    {
        if (!PatenteRegex.IsMatch(value))
        {
            throw new InvalidPresuncionPatenteException("La patente solo puede contener letras y números");
        }
    }
}