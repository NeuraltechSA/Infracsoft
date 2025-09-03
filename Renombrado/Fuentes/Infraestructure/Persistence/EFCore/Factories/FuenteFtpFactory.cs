using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;
using System.Text.Json;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;
sealed class FuenteFtpFactory
{
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

    private record FtpConfigData(
        string host,
        int port,
        string username,
        string password
    );
}