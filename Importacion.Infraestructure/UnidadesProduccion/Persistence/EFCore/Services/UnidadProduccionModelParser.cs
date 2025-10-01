using Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;
using Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Services;

public class UnidadProduccionModelParser : IModelParser<UnidadProduccion, UnidadProduccionModel>
{
    public UnidadProduccion ParseToEntity(UnidadProduccionModel model)
    {
        return UnidadProduccion.Create(
            model.Id.ToString(),
            model.Nombre
        );
    }

    public UnidadProduccionModel ParseToModel(UnidadProduccion entity)
    {
        return new UnidadProduccionModel
        {
            Id = new Guid(entity.Id.Value),
            Nombre = entity.Nombre.Value
        };
    }
}
