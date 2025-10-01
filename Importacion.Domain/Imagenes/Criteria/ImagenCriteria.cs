using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria;
using SharedKernel.Domain.Criteria.Order;

namespace Infracsoft.Importacion.Domain.Imagenes.Criteria;

public sealed class ImagenCriteria : BaseCriteria<ImagenCriteria>
{
    public ImagenCriteria(Filters filters, Orders orders, Pagination? pagination) 
        : base(filters, orders, pagination) { }
}