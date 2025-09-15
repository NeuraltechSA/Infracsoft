using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Factories;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Services;

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