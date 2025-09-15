using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Enums;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Models;

namespace RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Factories;

/// <summary>
/// Factory dispatcher para crear entidades de dominio a partir de modelos de persistencia.
/// Model → Entity
/// </summary>
internal static class PresuncionEntityFactory
{
    /// <summary>
    /// Crea una entidad de dominio Presuncion a partir de un modelo de persistencia.
    /// </summary>
    /// <param name="model">Modelo de persistencia</param>
    /// <returns>Entidad de dominio</returns>
    public static Presuncion Create(PresuncionModel model)
    {
        return model.TipoInfraccion switch
        {
            TipoPresuncion.Velocidad => PresuncionVelocidadEntityFactory.Create(model),
            _ => throw new ArgumentException($"Tipo de infracción desconocido: {model.TipoInfraccion}")
        };
    }
}