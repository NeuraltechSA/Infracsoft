namespace Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Repositories;

using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Domain.ValueObjects;
using Renombrado.Fuentes.Domain.Criteria;
using Renombrado.Fuentes.Domain.Contracts;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Services;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;
using global::SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using Renombrado.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;
using global::SharedKernel.Infraestructure.Persistence.EFCore.Services;

public class FuenteRepository : BaseEFCoreRepository<Fuente, FuenteId, FuenteModel, FuenteCriteria>, IFuenteRepository
{
    public FuenteRepository(RenombradoDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new FuenteModelParser())
    {
    }


}
