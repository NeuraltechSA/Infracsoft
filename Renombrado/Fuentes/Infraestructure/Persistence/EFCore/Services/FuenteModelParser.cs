using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Factories;
using System.Text.Json;
using SharedKernel.Infraestructure.Persistence.Contracts;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;

namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Services;

public class FuenteModelParser : IModelParser<Fuente, FuenteModel>
{
    public Fuente ParseToEntity(FuenteModel model)
    {
        return FuenteFactory.Create(model);
    }

    public FuenteModel ParseToModel(Fuente entity)
    {
        return new FuenteModel{
            Id = new Guid(entity.Id.Value),
            Nombre = entity.Nombre.Value,
            Descripcion = entity.Descripcion?.Value,
            Tipo = FuenteTipoMapper.FromEntity(entity),
            Config = JsonSerializer.Serialize(entity.ConfigToPrimitives())
        };
    }
}
