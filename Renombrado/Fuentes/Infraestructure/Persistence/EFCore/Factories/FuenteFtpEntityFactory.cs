using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;
using System.Text.Json;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;

/// <summary>
/// Factory específico para crear entidades FuenteFtp a partir de modelos de persistencia.
/// Model → Entity (específico)
/// </summary>
internal sealed class FuenteFtpEntityFactory
{
    /// <summary>
    /// Crea una entidad FuenteFtp a partir de un modelo de persistencia.
    /// </summary>
    /// <param name="model">Modelo de persistencia con configuración FTP en JSON</param>
    /// <returns>Entidad FuenteFtp</returns>
    public static FuenteFtp Create(FuenteModel model)
    {
        var config = JsonSerializer.Deserialize<FtpConfigData>(model.Config);
        EnsureConfigIsNotNull(config);
        return new FuenteFtp(
            model.Id.ToString(), 
            model.Descripcion, 
            model.Nombre,
            config!.host,
            config.port,
            config.username,
            config.password
        );
    }

    private static void EnsureConfigIsNotNull(FtpConfigData? config)
    {
        if (config == null)
        {
            throw new Exception("Config is null");
        }
    }

    /// <summary>
    /// Estructura de datos para deserializar la configuración FTP desde JSON.
    /// </summary>
    private record FtpConfigData(
        string host,
        int port,
        string username,
        string password
    );
}