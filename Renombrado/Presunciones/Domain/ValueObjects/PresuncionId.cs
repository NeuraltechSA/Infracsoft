using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Presunciones.Domain.ValueObjects;

public sealed record PresuncionId : UuidValueObject
{
    public PresuncionId(string value) : base(value)
    {
    }
}