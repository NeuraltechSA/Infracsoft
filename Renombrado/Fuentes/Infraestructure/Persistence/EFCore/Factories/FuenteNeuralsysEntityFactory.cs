using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;
using System.Text.Json;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;

/// <summary>
/// Factory específico para crear entidades FuenteNeuralsys a partir de modelos de persistencia.
/// Model → Entity (específico)
/// </summary>
internal sealed class FuenteNeuralsysEntityFactory
{
    /// <summary>
    /// Crea una entidad FuenteNeuralsys a partir de un modelo de persistencia.
    /// </summary>
    /// <param name="model">Modelo de persistencia con configuración Neuralsys en JSON</param>
    /// <returns>Entidad FuenteNeuralsys</returns>
    public static FuenteNeuralsys Create(FuenteModel model)
    {
        var config = JsonSerializer.Deserialize<NeuralsysConfigData>(model.Config);
        EnsureConfigIsNotNull(config);
        return new FuenteNeuralsys(
            model.Id.ToString(), 
            model.Nombre, 
            model.Descripcion,
            config!.url,
            config.usuario,
            config.contrasena
        );
    }

    private static void EnsureConfigIsNotNull(NeuralsysConfigData? config)
    {
        if (config == null)
        {
            throw new Exception("Config is null");
        }
    }

    /// <summary>
    /// Estructura de datos para deserializar la configuración Neuralsys desde JSON.
    /// </summary>
    private record NeuralsysConfigData(
        string url,
        string usuario,
        string contrasena
    );
}