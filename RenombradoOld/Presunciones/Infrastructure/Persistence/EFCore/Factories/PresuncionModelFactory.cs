using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Models;

namespace RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Factories;

/// <summary>
/// Factory dispatcher para crear modelos de persistencia a partir de entidades de dominio.
/// Entity → Model
/// </summary>
internal static class PresuncionModelFactory
{
    /// <summary>
    /// Crea un modelo de persistencia a partir de una entidad de dominio Presuncion.
    /// </summary>
    /// <param name="entity">Entidad de dominio</param>
    /// <returns>Modelo de persistencia</returns>
    public static PresuncionModel Create(Presuncion entity)
    {
        return entity switch
        {
            PresuncionVelocidad velocidad => PresuncionVelocidadModelFactory.Create(velocidad),
            _ => throw new ArgumentException($"Tipo de presunción desconocido: {entity.GetType().Name}")
        };
    }
}