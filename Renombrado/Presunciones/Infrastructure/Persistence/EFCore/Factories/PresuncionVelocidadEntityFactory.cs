using Renombrado.Presunciones.Domain.Entities;
using Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Models;

namespace Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Factories;

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
            model.FechaHora,
            model.Lugar,
            model.Patente,
            model.ValorMedido!.Value,
            model.ValorMaximo!.Value,
            model.Carril!.Value
        );
    }
}