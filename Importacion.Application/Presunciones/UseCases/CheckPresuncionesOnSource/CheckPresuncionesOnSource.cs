using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.CheckPresuncionesOnSource
{
    /// <summary>
    /// Caso de uso para verificar y procesar presunciones disponibles en la fuente.
    /// Este caso de uso es el punto de entrada del flujo de importación, encargándose de
    /// descubrir todas las presunciones disponibles en la fuente y publicar eventos
    /// para iniciar su procesamiento.
    /// </summary>
    public class CheckPresuncionesOnSource(
        IPresuncionSource fileSource,
        IEventBus eventBus)
    {
        private readonly IPresuncionSource _fileSource = fileSource;
        private readonly IEventBus _eventBus = eventBus;

        /// <summary>
        /// Ejecuta la verificación de presunciones en la fuente.
        /// Obtiene todas las rutas de presunciones disponibles y publica un evento
        /// PresuncionUploadedOnSourceEvent para cada una, iniciando el flujo de importación.
        /// </summary>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Execute()
        {
            var presuncionPaths = await _fileSource.GetPresuncionesPaths();
            foreach (var presuncionSourcePath in presuncionPaths)
            {
                await _eventBus.Publish(new PresuncionUploadedOnSourceEvent(presuncionSourcePath));
            }
        }
    }
}
