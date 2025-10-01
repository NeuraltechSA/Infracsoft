using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Enums;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Factories;

/// <summary>
/// Factory específico para crear modelos de persistencia a partir de entidades PresuncionVelocidad.
/// Entity → Model (específico)
/// </summary>
internal sealed class PresuncionVelocidadModelFactory
{
    /// <summary>
    /// Crea un modelo de persistencia a partir de una entidad PresuncionVelocidad.
    /// </summary>
    /// <param name="entity">Entidad PresuncionVelocidad</param>
    /// <returns>Modelo de persistencia con datos de velocidad</returns>
    public static PresuncionModel Create(PresuncionVelocidad entity)
    {
        return new PresuncionModel
        {
            Id = new Guid(entity.Id.Value),
            EquipoId = new Guid(entity.EquipoId.Value),
            FechaHora = entity.FechaHora?.Value,
            Lugar = entity.Lugar?.Value,
            Patente = entity.Patente?.Value,
            TipoInfraccion = TipoPresuncion.Velocidad,
            ValorMedido = entity.VelocidadMedida.Value,
            ValorMaximo = entity.VelocidadMaxima.Value,
            Carril = entity.Carril?.Value
        };
    }
}