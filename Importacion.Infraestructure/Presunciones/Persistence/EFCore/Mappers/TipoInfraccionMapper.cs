using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Enums;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Mappers;

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