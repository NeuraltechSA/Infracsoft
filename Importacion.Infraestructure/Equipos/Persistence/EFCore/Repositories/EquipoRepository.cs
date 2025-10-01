using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Criteria;
using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Domain.Equipos.ValueObjects;
using Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Services;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;

namespace Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Repositories;

public sealed class EquipoRepository : BaseEFCoreRepository<Equipo, EquipoId, EquipoModel, EquipoCriteria>, IEquipoRepository
{
    public EquipoRepository(ImportacionDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new EquipoModelParser())
    {
    }
}
