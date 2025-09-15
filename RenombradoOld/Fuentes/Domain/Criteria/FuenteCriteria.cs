using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Order;

namespace RenombradoOld.Fuentes.Domain.Criteria;

public sealed class FuenteCriteria : BaseCriteria
{
    public FuenteCriteria(Filters filters, Orders orders, Pagination? pagination) 
        : base(filters, orders, pagination) { }
    
}