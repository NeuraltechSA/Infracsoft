using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    public record PresuncionTempFilesStoredEvent(
        string PresuncionStoreKey
    ) : DomainEvent
    {
        public override string MessageName => "infracsoft.importacion.presuncion.presuncion_temp_files_stored";
    }
}
