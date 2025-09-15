using SharedKernel.Domain.ValueObjects;

namespace RenombradoOld.Presunciones.Domain.ValueObjects;

public sealed record PresuncionId : UuidValueObject
{
    public PresuncionId(string value) : base(value)
    {
    }
}