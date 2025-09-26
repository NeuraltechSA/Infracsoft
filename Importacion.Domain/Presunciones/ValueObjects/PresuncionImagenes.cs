using SharedKernel.Domain.ValueObjects;
using System.Collections.Generic;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects
{
    public record PresuncionImagenes : ListValueObject<PresuncionImagenId>
    {
        public static readonly int MaxImages = 5;
        public static readonly int MinImages = 1;

        public PresuncionImagenes(IReadOnlyList<PresuncionImagenId> Value) : base(Value)
        {
            EnsureMinImages(Value);
            EnsureMaxImages(Value);
        }

        private static void EnsureMinImages(IReadOnlyList<PresuncionImagenId> value)
        {
            if (value.Count < MinImages)
            {
                throw InvalidPresuncionImagenesException.CreateMinImages(MinImages, value.Count);
            }
        }

        private static void EnsureMaxImages(IReadOnlyList<PresuncionImagenId> value)
        {
            if (value.Count > MaxImages)
            {
                throw InvalidPresuncionImagenesException.CreateMaxImages(MaxImages, value.Count);
            }
        }
    }
}
