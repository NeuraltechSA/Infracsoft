using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Filter;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Criteria;

public class UnidadProduccionCriteria : BaseCriteria<UnidadProduccionCriteria>
{
    public static UnidadProduccionCriteria Create() => new UnidadProduccionCriteria();
    
    public UnidadProduccionCriteria WithNombre(string nombre)
    {
        AddFilter(new Filter("Nombre", FilterOperators.CONTAINS, nombre));
        return this;
    }
    
}
