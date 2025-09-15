namespace SharedKernel.Domain.Criteria.Order;

public sealed record Order
{
    private readonly OrderBy _orderBy;
    private readonly OrderType _orderType;

    public Order(OrderBy orderBy, OrderType orderType){
        _orderBy = orderBy;
        _orderType = orderType;
    }
    
    public string GetOrderBy() => _orderBy.Value;
    public OrderTypes GetOrderType() => _orderType.Value;
};