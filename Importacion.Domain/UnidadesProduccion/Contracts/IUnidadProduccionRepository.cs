using Infracsoft.Importacion.Domain.UnidadesProduccion.Criteria;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;
using Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Contracts;

public interface IUnidadProduccionRepository : IRepository<UnidadProduccion, UnidadProduccionId, UnidadProduccionCriteria>
{
}
