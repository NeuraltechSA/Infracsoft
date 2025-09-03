using Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Enums;

namespace Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Mappers;

public static class TipoInfraccionMapper
{
    public static TipoPresuncion GetTipoInfraccion(string entityType)
    {
        return entityType switch
        {
            "PresuncionVelocidad" => TipoPresuncion.Velocidad,
            _ => throw new ArgumentException($"Tipo de entidad desconocido: {entityType}")
        };
    }
}