namespace SharedKernel.Domain.Criteria.Filter;

public sealed class Filter
{
    private readonly FilterField _field;
    private readonly FilterOperator _operator;
    private readonly FilterValue _value;

    public Filter(string field, FilterOperators @operator, string value)
    {
        _field = new FilterField(field);
        _operator = new FilterOperator(@operator);
        _value = new FilterValue(value);
    }

    public string GetValue() => _value.Value;
    public string GetField() => _field.Value;
    public FilterOperators GetOperator() => _operator.Value;
}