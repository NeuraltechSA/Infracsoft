using System;
using System.Text.RegularExpressions;
using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Fuentes.Domain.ValueObjects.Ftp;

public sealed record FuenteFtpHost : StringValueObject
{
    private const string HOST_PATTERN = @"^(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}|localhost|(\d{1,3}\.){3}\d{1,3})$";

    public FuenteFtpHost(string value) : base(value)
    {
        EnsureValidHost(value);
    }

    private void EnsureValidHost(string value)
    {
        if (!Regex.IsMatch(value, HOST_PATTERN))
        {
            throw new ArgumentException("El host FTP debe ser un dominio válido o dirección IP");
        }
    }

}