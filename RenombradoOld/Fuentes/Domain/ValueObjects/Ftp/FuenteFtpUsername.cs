using System;
using SharedKernel.Domain.ValueObjects;

namespace RenombradoOld.Fuentes.Domain.ValueObjects.Ftp;

public sealed record FuenteFtpUsername : StringValueObject
{
    private const int MaxLength = 255;

    public FuenteFtpUsername(string value) : base(value)
    {
        EnsureValidLength(value);
    }

    private void EnsureValidLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"El nombre de usuario FTP no puede exceder {MaxLength} caracteres");
        }
    }

}