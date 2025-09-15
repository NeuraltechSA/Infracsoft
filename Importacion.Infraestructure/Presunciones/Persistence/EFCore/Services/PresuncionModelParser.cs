using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Factories;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Services;

public class PresuncionModelParser : IModelParser<Presuncion, PresuncionModel>
{
    public Presuncion ParseToEntity(PresuncionModel model)
    {
        return PresuncionEntityFactory.Create(model);
    }

    public PresuncionModel ParseToModel(Presuncion entity)
    {
        return PresuncionModelFactory.Create(entity);
    }
}