using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria.Order;
using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.Criteria;

public sealed class PresuncionCriteria : BaseCriteria
{
    public PresuncionCriteria(Filters filters, Orders orders, Pagination? pagination)
    : base(filters, orders, pagination) { }
}