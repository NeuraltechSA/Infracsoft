namespace SharedKernel.Domain.ValueObjects;

public record FloatValueObject(float Value) : ValueObject<float>(Value);