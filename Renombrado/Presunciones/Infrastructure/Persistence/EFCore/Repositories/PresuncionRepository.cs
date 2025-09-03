using Renombrado.Presunciones.Domain.Contracts;
using Renombrado.Presunciones.Domain.Criteria;
using Renombrado.Presunciones.Domain.Entities;
using Renombrado.Presunciones.Domain.ValueObjects;
using Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Models;
using Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Services;
using Renombrado.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using SharedKernel.Infraestructure.Persistence.EFCore.Services;

namespace Renombrado.Presunciones.Infrastructure.Persistence.EFCore.Repositories;

public sealed class PresuncionRepository : BaseEFCoreRepository<Presuncion, PresuncionId, PresuncionModel, PresuncionCriteria>, IPresuncionRepository
{
    public PresuncionRepository(RenombradoDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new PresuncionModelParser())
    {
    }
}