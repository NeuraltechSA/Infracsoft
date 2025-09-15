using RenombradoOld.Presunciones.Domain.Contracts;
using RenombradoOld.Presunciones.Domain.Criteria;
using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Domain.ValueObjects;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Models;
using RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Services;
using RenombradoOld.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using SharedKernel.Infraestructure.Persistence.EFCore.Services;

namespace RenombradoOld.Presunciones.Infrastructure.Persistence.EFCore.Repositories;

public sealed class PresuncionRepository : BaseEFCoreRepository<Presuncion, PresuncionId, PresuncionModel, PresuncionCriteria>, IPresuncionRepository
{
    public PresuncionRepository(RenombradoDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new PresuncionModelParser())
    {
    }
}