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
    public class CheckPresuncionesOnSource(
        IPresuncionSource fileSource,
        IEventBus eventBus)
    {
        private readonly IPresuncionSource _fileSource = fileSource;
        private readonly IEventBus _eventBus = eventBus;

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
