using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

public sealed record PresuncionId : UuidValueObject
{
    public PresuncionId(string value) : base(value)
    {
    }
}