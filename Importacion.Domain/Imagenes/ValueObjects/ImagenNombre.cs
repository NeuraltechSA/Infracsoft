using System;
using System.Text.RegularExpressions;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Imagenes.ValueObjects;

public sealed record ImagenNombre : StringValueObject
{
    private const int MinLength = 1;
    private const int MaxLength = 255;

    public ImagenNombre(string value) : base(value)
    {
        EnsureMinLength(value);
        EnsureMaxLength(value);
        EnsureValidFormat(value);
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"El nombre de la imagen no puede exceder {MaxLength} caracteres");
        }
    }

    private static void EnsureMinLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new ArgumentException($"El nombre de la imagen debe tener al menos {MinLength} caracter");
        }
    }

    private static void EnsureValidFormat(string value)
    {

        var extension = Path.GetExtension(value).ToLowerInvariant();
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
        
        if (!string.IsNullOrEmpty(extension) && !Array.Exists(validExtensions, ext => ext == extension))
        {
            throw new ArgumentException($"La extensión '{extension}' no es válida. Extensiones permitidas: {string.Join(", ", validExtensions)}");
        }
    }
}