using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Events
{
    /// <summary>
    /// Evento de dominio que se publica cuando se detecta una nueva presunción en la fuente.
    /// Este evento inicia el flujo de importación de presunciones, activando la descarga
    /// y almacenamiento temporal de los archivos de la presunción.
    /// </summary>
    /// <param name="PresuncionSourcePath">Ruta de la presunción en la fuente original.</param>
    public record PresuncionUploadedOnSourceEvent (
        string PresuncionSourcePath
    ) : DomainEvent
    {
        /// <summary>
        /// Nombre del mensaje del evento para identificación en el bus de eventos.
        /// </summary>
        public override string MessageName => "infracsoft.importacion.presuncion.uploaded_on_source";
    }
}
