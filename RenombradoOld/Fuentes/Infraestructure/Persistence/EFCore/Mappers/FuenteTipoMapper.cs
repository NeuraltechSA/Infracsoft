using RenombradoOld.Fuentes.Domain.Entities;
using RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Enums;


sealed class FuenteTipoMapper
{
    public static FuenteTipo FromEntity(Fuente entity)
    {
        return entity.GetType().Name switch
        {
            "FuenteFtp" => FuenteTipo.FTP,
            "FuenteNeuralsys" => FuenteTipo.Neuralsys,
            _ => throw new Exception("Tipo de fuente desconocido"),
        };
    }
}