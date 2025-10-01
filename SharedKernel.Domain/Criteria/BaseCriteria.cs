using System.Collections.Generic;
using System.Linq;
using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria.Order;

namespace SharedKernel.Domain.Criteria;

public class BaseCriteria<T> where T : BaseCriteria<T>
{
    protected Filters Filters { get; private set; }
    protected Orders Orders { get; private set; }
    protected Pagination? Pagination { get; private set; }

    public BaseCriteria(Filters? filters = null, Orders? orders = null, Pagination? pagination = null)
    {
        Filters = filters ?? new Filters([]);
        Orders = orders ?? new Orders([]);
        Pagination = pagination;
    }

    public T AddFilter(Filter.Filter newFilter)
    {
        var newFilters = new List<Filter.Filter>(Filters.Value) { newFilter };
        Filters = new Filters(newFilters);
        return (T)this;
    }

    public T AddOrder(Order.Order newOrder)
    {
        var newOrders = new List<Order.Order>(Orders.Value) { newOrder };
        Orders = new Orders(newOrders);
        return (T)this;
    }

    public T Paginate(int page, int pageSize)
    {
        Pagination = new Pagination(page, pageSize);
        return (T)this;
    }

    public List<Filter.Filter> GetFilters() => Filters.Value;

    public List<Order.Order> GetOrders() => Orders.Value;

    public bool HasPagination => Pagination != null;

    public int? GetPageSize() => Pagination?.Size;

    public int? GetPageNumber() => Pagination?.Page;

    public int? GetPageNumberFromZero() => Pagination?.Page != null ? Pagination.Page - 1 : null;
}