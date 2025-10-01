using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;

public sealed record UnidadProduccionId : UuidValueObject
{
    public UnidadProduccionId(string value) : base(value)
    {
    }
}
