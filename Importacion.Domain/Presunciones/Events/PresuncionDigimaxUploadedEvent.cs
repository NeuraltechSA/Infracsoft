using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
     public record PresuncionDigimaxUploadedEvent (
        string CompressedFileSourcePath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.digimax.uploaded";
    }
}
