using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando los archivos de una presunción han sido
    /// almacenados temporalmente. Este evento activa el proceso de importación de la presunción.
    /// </summary>
    /// <param name="PresuncionSourcePath">Ruta original de la presunción en la fuente.</param>
    /// <param name="PresuncionDestinationPath">Ruta de destino donde se guardaron los archivos temporalmente.</param>
    public record PresuncionTempFilesStoredEvent(
        string PresuncionSourcePath,
        string PresuncionDestinationPath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.temp_files_stored";
    }
}
