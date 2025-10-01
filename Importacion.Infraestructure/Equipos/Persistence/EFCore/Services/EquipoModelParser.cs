using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Services;

public class EquipoModelParser : IModelParser<Equipo, EquipoModel>
{
    public Equipo ParseToEntity(EquipoModel model)
    {
        return Equipo.Create(
            model.Id.ToString(),
            model.Nombre,
            model.UnidadProduccionId.ToString()
        );
    }

    public EquipoModel ParseToModel(Equipo entity)
    {
        return new EquipoModel
        {
            Id = new Guid(entity.Id.Value),
            Nombre = entity.Nombre.Value,
            UnidadProduccionId = new Guid(entity.UnidadProduccionId.Value)
        };
    }
}
