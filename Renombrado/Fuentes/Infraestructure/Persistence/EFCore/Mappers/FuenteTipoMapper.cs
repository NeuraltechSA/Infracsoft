using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Enums;


sealed class FuenteTipoMapper
{
    public static FuenteTipo FromEntity(Fuente entity)
    {
        return entity.GetType().Name switch
        {
            "FuenteFtp" => FuenteTipo.FTP,
            _ => throw new Exception("Tipo de fuente desconocido"),
        };
    }
}