using SharedKernel.Domain.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects
{
    public record PresuncionImagenId : UuidValueObject
    {
        public PresuncionImagenId(string value) : base(value)
        {
        }
    }
}
