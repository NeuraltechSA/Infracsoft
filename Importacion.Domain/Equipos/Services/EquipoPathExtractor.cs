using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Criteria;
using Infracsoft.Importacion.Domain.Equipos.Exceptions;
using Infracsoft.Importacion.Domain.Equipos.ValueObjects;

namespace Infracsoft.Importacion.Domain.Equipos.Services;

/// <summary>
/// Servicio que extrae información del equipo desde la ruta de archivos de presunciones.
/// Los archivos deben estar en una carpeta que comience con "e-" seguido del nombre del equipo.
/// Funciona para cualquier tipo de importación de presunciones por ruta (Digimax, etc.).
/// </summary>
public class EquipoPathExtractor
{
    private readonly IEquipoRepository _equipoRepository;

    public EquipoPathExtractor(IEquipoRepository equipoRepository)
    {
        _equipoRepository = equipoRepository;
    }

    /// <summary>
    /// Extrae el equipo completo desde la ruta del archivo de presunción.
    /// Busca una carpeta que comience con "e-" y extrae el nombre del equipo.
    /// </summary>
    /// <param name="fileSourcePath">Ruta completa del archivo de presunción.</param>
    /// <returns>Equipo completo encontrado en la ruta.</returns>
    /// <exception cref="EquipoNotExistsException">Se lanza cuando no se encuentra un equipo con el nombre extraído.</exception>
    public async Task<Entities.Equipo> ExtractEquipoFromPath(string fileSourcePath)
    {
        var equipoNombre = ExtractEquipoNombreFromPath(fileSourcePath);
        var criteria = EquipoCriteria.Create().WithNombre(equipoNombre);
        var equipos = await _equipoRepository.Find(criteria);
        
        EnsureEquipoExists(equipos, equipoNombre);
        return equipos.First();
    }

    /// <summary>
    /// Extrae el nombre del equipo desde la ruta del archivo.
    /// Busca en todos los segmentos de la ruta una carpeta que comience con "e-".
    /// Detecta automáticamente el separador de ruta utilizado.
    /// </summary>
    /// <param name="path">Ruta completa del archivo.</param>
    /// <returns>Nombre del equipo extraído de la carpeta "e-".</returns>
    private string ExtractEquipoNombreFromPath(string path)
    {
        var separator = DetectPathSeparator(path);
        var pathSegments = path.Split(separator);
        
        foreach (var segment in pathSegments)
        {
            if (segment.StartsWith("e-", StringComparison.OrdinalIgnoreCase))
            {
                return segment.Substring(2); // Remover "e-" del inicio
            }
        }
        
        throw new InvalidOperationException($"No se encontró una carpeta que comience con 'e-' en la ruta: {path}");
    }

    /// <summary>
    /// Detecta automáticamente el separador de ruta utilizado en la cadena.
    /// </summary>
    /// <param name="path">Ruta a analizar.</param>
    /// <returns>El carácter separador detectado.</returns>
    private char DetectPathSeparator(string path)
    {
        if (path.Contains('/'))
            return '/';
        
        if (path.Contains('\\'))
            return '\\';
        
        // Si no encuentra separadores, usar el del sistema
        return Path.DirectorySeparatorChar;
    }

    private void EnsureEquipoExists(IEnumerable<Entities.Equipo> equipos, string equipoNombre)
    {
        if (!equipos.Any())
        {
            throw EquipoNotExistsException.CreateFromNombre(equipoNombre);
        }
    }
}
