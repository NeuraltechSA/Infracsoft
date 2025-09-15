using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    public record PresuncionImportedEvent(
        string PresuncionId,
        string PresuncionSourcePath,
        string PresuncionTempStoreKey
    ) : DomainEvent
    {
        public override string MessageName => "infracsoft.importacion.presuncion.imported";
    }
}
