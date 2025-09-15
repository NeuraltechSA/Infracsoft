using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Criteria;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Services;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Repositories;

public sealed class PresuncionRepository : BaseEFCoreRepository<Presuncion, PresuncionId, PresuncionModel, PresuncionCriteria>, IPresuncionRepository
{
    public PresuncionRepository(ImportacionDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new PresuncionModelParser())
    {
    }
}