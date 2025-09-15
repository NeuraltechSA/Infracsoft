using System;
using System.IO;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Imagenes.ValueObjects;

public sealed record ImagenRuta : StringValueObject
{
    private const int MaxLength = 500;

    public ImagenRuta(string value) : base(value)
    {
        EnsureMaxLength(value);
        EnsureValidPath(value);
    }

    private static void EnsureMaxLength(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"La ruta de la imagen no puede exceder {MaxLength} caracteres");
        }
    }

    private static void EnsureValidPath(string value)
    {
        try
        {
            var fileName = Path.GetFileName(value);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("La ruta debe incluir un nombre de archivo válido");
            }
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new ArgumentException("La ruta especificada no es válida");
        }
    }
}