using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Imagenes.ValueObjects;

public sealed record ImagenId : UuidValueObject
{
    public ImagenId(string value) : base(value)
    {
    }
}