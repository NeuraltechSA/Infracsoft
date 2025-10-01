using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria.Order;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.Criteria;

public sealed class PresuncionCriteria : BaseCriteria<PresuncionCriteria>
{
    public PresuncionCriteria(Filters filters, Orders orders, Pagination? pagination)
    : base(filters, orders, pagination) { }

    public static PresuncionCriteria Create() => new PresuncionCriteria(new Filters([]), new Orders([]), null);
    
    public PresuncionCriteria WithFechaHora(DateTime fechaHora)
    {
        AddFilter(new Filter("FechaHora", FilterOperators.EQ, fechaHora));
        return this;
    }
    
    public PresuncionCriteria WithLugar(string lugar)
    {
        AddFilter(new Filter("Lugar", FilterOperators.EQ, lugar));
        return this;
    }
    
    public PresuncionCriteria WithEquipoId(string equipoId)
    {
        AddFilter(new Filter("EquipoId", FilterOperators.EQ, equipoId));
        return this;
    }
}