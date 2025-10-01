using Infracsoft.Importacion.Domain.Presunciones.DTO;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

/// <summary>
/// Servicio que extrae datos de un archivo Digimax para verificación y procesamiento.
/// Útil para validaciones rápidas y verificación de duplicados.
/// </summary>
public class DigimaxDataExtractor
{

    /// <summary>
    /// Extrae los datos directamente del archivo Digimax comprimido.
    /// </summary>
    /// <param name="compressedFileSourcePath">Ruta del archivo comprimido de Digimax.</param>
    /// <returns>DTO con los datos extraídos del archivo.</returns>
    public DigimaxDataDTO ExtractData(string compressedFileSourcePath)
    {
        return ExtractDataFromFilename(compressedFileSourcePath);
    }

    /// <summary>
    /// Extrae los datos del archivo basándose únicamente en el nombre del archivo.
    /// </summary>
    /// <param name="path">Ruta del archivo.</param>
    /// <returns>DTO con los datos extraídos del nombre del archivo.</returns>
    private DigimaxDataDTO ExtractDataFromFilename(string path)
    {
        var regex = new Regex(PresuncionVelocidad.DigimaxFilenameRegex);
        var match = regex.Match(Path.GetFileName(path));
        
        if (!match.Success)
            throw new InvalidOperationException($"Invalid filename: {Path.GetFileName(path)}");
        
        var lugar = match.Groups["lugar"].Value;
        var fecha = DateTime.ParseExact($"{match.Groups["fecha"].Value} {match.Groups["hora"].Value}", "dd-MM-yyyy HH.mm.ss", CultureInfo.InvariantCulture);
        var maxima = float.Parse(match.Groups["maxima"].Value);
        var medida = float.Parse(match.Groups["medida"].Value);
        
        return new DigimaxDataDTO(
            fecha,
            lugar,
            medida,
            maxima,
            path
        );
    }
}
