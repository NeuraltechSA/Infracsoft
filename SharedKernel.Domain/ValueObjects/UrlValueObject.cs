using System.Text.RegularExpressions;
using SharedKernel.Domain.Exceptions;

namespace SharedKernel.Domain.ValueObjects;

public partial record UrlValueObject : StringValueObject
{
    private const string URL_PATTERN = @"https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)\n";

    public UrlValueObject(string value) : base(value)
    {
        EnsureIsValidUrl(value);
    }

    private void EnsureIsValidUrl(string value)
    {
        if (!Regex.IsMatch(value, URL_PATTERN))
        {
            throw new InvalidUrlException($"La URL no es válida: {value}");
        }
    }
}