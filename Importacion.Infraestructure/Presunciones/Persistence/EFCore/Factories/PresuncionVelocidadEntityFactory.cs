using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Factories;

/// <summary>
/// Factory específico para crear entidades PresuncionVelocidad a partir de modelos de persistencia.
/// Model → Entity (específico)
/// </summary>
internal sealed class PresuncionVelocidadEntityFactory
{
    /// <summary>
    /// Crea una entidad PresuncionVelocidad a partir de un modelo de persistencia.
    /// </summary>
    /// <param name="model">Modelo de persistencia con datos de velocidad</param>
    /// <returns>Entidad PresuncionVelocidad</returns>
    public static PresuncionVelocidad Create(PresuncionModel model)
    {
        return new PresuncionVelocidad(
            model.Id.ToString(),
            model.EquipoId.ToString(),
            new List<string>(), // Las imágenes se cargan por separado
            model.ValorMedido!.Value,
            model.ValorMaximo!.Value,
            model.Carril,
            model.FechaHora,
            model.Lugar,
            model.Patente
        );
    }
}