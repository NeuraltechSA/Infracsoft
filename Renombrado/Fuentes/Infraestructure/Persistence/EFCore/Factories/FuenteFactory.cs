using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Enums;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;

class FuenteFactory
{
    public static Fuente Create(FuenteModel model)
    {
        return model.Tipo switch
        {
            FuenteTipo.FTP => FuenteFtpFactory.Create(model),
            _ => throw new Exception("Tipo de fuente no soportado"),
        };
    }
}