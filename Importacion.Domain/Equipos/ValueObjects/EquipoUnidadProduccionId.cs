using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Equipos.ValueObjects;

public sealed record EquipoUnidadProduccionId : UuidValueObject
{
    public EquipoUnidadProduccionId(string value) : base(value)
    {
    }
}
