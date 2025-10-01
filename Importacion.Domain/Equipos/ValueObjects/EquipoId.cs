using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Equipos.ValueObjects;

public sealed record EquipoId : UuidValueObject
{
    public EquipoId(string value) : base(value)
    {
    }
}
