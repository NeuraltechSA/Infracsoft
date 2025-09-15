using SharedKernel.Domain.ValueObjects;
using System;

namespace RenombradoOld.Fuentes.Domain.ValueObjects.Ftp;

public sealed record FuenteFtpPassword : StringValueObject
{
    private const int MinLength = 1;
    private const int MaxLength = 255;


    public FuenteFtpPassword(string value) : base(value)
    {
        EnsureValidLength(value);
    }

    private void EnsureValidLength(string value)
    {
        if (value.Length < MinLength || value.Length > MaxLength)
        {
            throw new ArgumentException($"La contraseña FTP debe tener entre {MinLength} y {MaxLength} caracteres");
        }
    }

}