namespace RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Repositories;

using RenombradoOld.Fuentes.Domain.Entities;
using RenombradoOld.Fuentes.Domain.ValueObjects;
using RenombradoOld.Fuentes.Domain.Criteria;
using RenombradoOld.Fuentes.Domain.Contracts;
using RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Services;
using RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Models;
using global::SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using RenombradoOld.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;
using global::SharedKernel.Infraestructure.Persistence.EFCore.Services;

public class FuenteRepository : BaseEFCoreRepository<Fuente, FuenteId, FuenteModel, FuenteCriteria>, IFuenteRepository
{
    public FuenteRepository(RenombradoDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new FuenteModelParser())
    {
    }


}
