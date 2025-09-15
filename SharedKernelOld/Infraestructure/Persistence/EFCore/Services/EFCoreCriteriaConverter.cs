using SharedKernel.Domain.Criteria;
using System.Linq.Dynamic.Core;
using SharedKernel.Domain.Criteria.Filter;
using SharedKernel.Domain.Criteria.Order;

namespace SharedKernel.Infraestructure.Persistence.EFCore.Services;

public class EFCoreCriteriaConverter
{
    private string ParseFilter(Filter filter)
    {
        return filter.GetOperator() switch
        {
            FilterOperators.EQ => $"{filter.GetField()} == @0",
            FilterOperators.NEQ => $"{filter.GetField()} != @0",
            FilterOperators.GT => $"{filter.GetField()} > @0",
            FilterOperators.LT => $"{filter.GetField()} < @0",
            FilterOperators.GTE => $"{filter.GetField()} >= @0",
            FilterOperators.LTE => $"{filter.GetField()} <= @0",
            FilterOperators.CONTAINS => $"{filter.GetField()}.Contains(@0)",
            FilterOperators.NOT_CONTAINS => $"{filter.GetField()}.NotContains(@0)",
            FilterOperators.STARTS_WITH => $"{filter.GetField()}.StartsWith(@0)",
            FilterOperators.ENDS_WITH => $"{filter.GetField()}.EndsWith(@0)",
            _ => throw new Exception("Invalid operator")
        };
    }

    private string ParseOrders(List<Order> orders)
    {
        return string.Join(", ", orders.Select(order => $"{order.GetOrderBy()} {(order.GetOrderType() == OrderTypes.ASC ? "asc" : "desc")}"));
    }

    private IQueryable<TModel> ApplyFilters<TModel>(BaseCriteria criteria, IQueryable<TModel> query)
    {
        foreach (var filter in criteria.GetFilters())
        {
            query = query.Where(ParseFilter(filter));
        }
        return query;
    }

    private IQueryable<TModel> ApplyOrders<TModel>(BaseCriteria criteria, IQueryable<TModel> query)
    {
        query = query.OrderBy(ParseOrders(criteria.GetOrders()));
        return query;
    }

    private IQueryable<TModel> ApplyPagination<TModel>(BaseCriteria criteria, IQueryable<TModel> query)
    {
        if (!criteria.HasPagination) return query;
        
        var pageNumber = (int)criteria.GetPageNumberFromZero()!;
        var pageSize = (int)criteria.GetPageSize()!;
        query = query.Skip(pageNumber * pageSize).Take(pageSize);
        return query;
    }


    public IQueryable<TModel> Apply<TModel>(BaseCriteria criteria, IQueryable<TModel> query)
    {
        query = ApplyFilters(criteria, query);
        query = ApplyOrders(criteria, query);
        query = ApplyPagination(criteria, query);
        return query;
    }
}