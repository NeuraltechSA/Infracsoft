using Infracsoft.Importacion.Domain.UnidadesProduccion.Contracts;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Criteria;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;
using Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;
using Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Services;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;

namespace Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Repositories;

public sealed class UnidadProduccionRepository : BaseEFCoreRepository<UnidadProduccion, UnidadProduccionId, UnidadProduccionModel, UnidadProduccionCriteria>, IUnidadProduccionRepository
{
    public UnidadProduccionRepository(ImportacionDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new UnidadProduccionModelParser())
    {
    }
}
