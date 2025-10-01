using Infracsoft.Importacion.Domain.Equipos.ValueObjects;
using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Filter;

namespace Infracsoft.Importacion.Domain.Equipos.Criteria;

public class EquipoCriteria : BaseCriteria<EquipoCriteria>
{
    public static EquipoCriteria Create() => new EquipoCriteria();
    
    public EquipoCriteria WithUnidadProduccionId(EquipoUnidadProduccionId unidadProduccionId)
    {
        AddFilter(new Filter("UnidadProduccionId", FilterOperators.EQ, unidadProduccionId.Value));
        return this;
    }
    
    public EquipoCriteria WithNombre(string nombre)
    {
        AddFilter(new Filter("Nombre", FilterOperators.EQ, nombre));
        return this;
    }
    
}
