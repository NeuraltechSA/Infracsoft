using SharedKernel.Domain.ValueObjects;
using Renombrado.Fuentes.Domain.Exceptions;

namespace Renombrado.Fuentes.Domain.ValueObjects.Neuralsys;

public sealed record FuenteNeuralsysUrl : UrlValueObject
{
    public FuenteNeuralsysUrl(string value) : base(value)
    {
        EnsureIsHttpsUrl(value);
    }

    private static void EnsureIsHttpsUrl(string value)
    {
        if (!value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidFuenteNeuralsysUrlException("La URL de Neuralsys debe usar HTTPS");
        }
    }
}