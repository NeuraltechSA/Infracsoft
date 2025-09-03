using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria.Order;
using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Presunciones.Domain.Criteria;

public sealed class PresuncionCriteria : BaseCriteria
{
    public PresuncionCriteria(
        FilterCollection? filters = null,
        OrderCollection? orders = null,
        int? limit = null,
        int? offset = null)
        : base(filters, orders, limit, offset)
    {
    }
}