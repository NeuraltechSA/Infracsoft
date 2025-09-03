using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Enums;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;

/// <summary>
/// Factory dispatcher para crear entidades de dominio a partir de modelos de persistencia.
/// Model → Entity
/// </summary>
internal static class FuenteEntityFactory
{
    /// <summary>
    /// Crea una entidad de dominio Fuente a partir de un modelo de persistencia.
    /// </summary>
    /// <param name="model">Modelo de persistencia</param>
    /// <returns>Entidad de dominio</returns>
    public static Fuente Create(FuenteModel model)
    {
        return model.Tipo switch
        {
            FuenteTipo.FTP => FuenteFtpEntityFactory.Create(model),
            FuenteTipo.Neuralsys => FuenteNeuralsysEntityFactory.Create(model),
            _ => throw new Exception("Tipo de fuente no soportado"),
        };
    }
}