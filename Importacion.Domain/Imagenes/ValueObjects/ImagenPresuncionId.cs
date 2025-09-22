using SharedKernel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Imagenes.ValueObjects
{
    public sealed record ImagenPresuncionId : UuidValueObject
    {
        public ImagenPresuncionId(string value) : base(value)
        {
        }
    }
}
