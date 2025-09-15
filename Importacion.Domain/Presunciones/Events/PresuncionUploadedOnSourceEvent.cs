using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    public record PresuncionUploadedOnSourceEvent (
        string PresuncionSourcePath
    ) :DomainEvent
    {
        public override string MessageName => "infracsoft.importacion.presuncion.uploaded_on_source";
    }
}
