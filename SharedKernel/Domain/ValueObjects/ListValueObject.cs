using System.Collections.Generic;

namespace SharedKernel.Domain.ValueObjects;

public record ListValueObject<T>(IReadOnlyList<T> Value) : ValueObject<IReadOnlyList<T>>(Value);