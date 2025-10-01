using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

public sealed record PresuncionEquipoId : UuidValueObject
{
    public PresuncionEquipoId(string value) : base(value)
    {
    }
}
