using System;

namespace SharedKernel.Domain.ValueObjects;

public record DateTimeValueObject(DateTime Value) : ValueObject<DateTime>(Value);