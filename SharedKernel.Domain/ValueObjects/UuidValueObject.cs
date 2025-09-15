using System;
using System.Text.RegularExpressions;

namespace SharedKernel.Domain.ValueObjects;

public abstract record UuidValueObject : StringValueObject
{
    private const string REGEX_UUID = @"^[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}$";
    protected UuidValueObject(string value) : base(value)
    {
        EnsureIsValidGuid(value);
    }


    private void EnsureIsValidGuid(string value)
    {
        if (!Regex.IsMatch(value, REGEX_UUID))
        {
            throw new ArgumentException($"Invalid UUID format: {value}");
        }
    }
}