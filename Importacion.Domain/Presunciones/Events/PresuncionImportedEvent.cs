using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando una presunción ha sido exitosamente importada
    /// a la base de datos. Este evento activa el almacenamiento permanente de los archivos
    /// asociados a la presunción.
    /// </summary>
    /// <param name="PresuncionId">ID único de la presunción importada.</param>
    /// <param name="PresuncionSourcePath">Ruta original de la presunción en la fuente.</param>
    /// <param name="PresuncionDestinationPath">Ruta de destino con los archivos de la presunción.</param>
    public record PresuncionImportedEvent(
        string PresuncionId,
        string PresuncionSourcePath,
        string PresuncionDestinationPath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.imported";
    }
}
