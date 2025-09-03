using System;
using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Fuentes.Domain.ValueObjects.Ftp;

public sealed record FuenteFtpPort : IntValueObject
{
    private const int MinPort = 21;
    private const int MaxPort = 65535;

    public FuenteFtpPort(int value) : base(value)
    {
        EnsureValidPort(value);
    }

    private void EnsureValidPort(int port)
    {
        if (port < MinPort || port > MaxPort)
        {
            throw new ArgumentException($"El puerto FTP debe estar entre {MinPort} y {MaxPort}");
        }
    }
}