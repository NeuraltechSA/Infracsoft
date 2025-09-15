using System;

namespace SharedKernel.Domain.ValueObjects;

public abstract record StringValueObject : ValueObject<string>
{
    public StringValueObject(string value) : base(value)
    {
        EnsureIsNotEmpty(value);
    }
    private void EnsureIsNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede estar vacío");
        }
    }
}