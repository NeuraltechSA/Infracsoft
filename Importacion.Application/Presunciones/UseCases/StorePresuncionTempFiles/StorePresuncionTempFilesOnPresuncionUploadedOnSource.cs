using Infracsoft.Importacion.Application.Presunciones.UseCases.ParsePresuncionFromFile;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.StorePresuncionTempFiles
{
    /// <summary>
    /// Event handler que reacciona al evento PresuncionUploadedOnSourceEvent.
    /// Este handler inicia el proceso de descarga y almacenamiento temporal de archivos
    /// cuando se detecta una nueva presunción en la fuente.
    /// </summary>
    public class StoreTempPresuncionFilesOnPresuncionUploadedOnSource(StorePresuncionTempFilesUseCase useCase) : IConsumer<PresuncionUploadedOnSourceEvent>
    {
        private readonly StorePresuncionTempFilesUseCase _useCase = useCase;

        /// <summary>
        /// Procesa el evento de presunción subida a la fuente.
        /// Ejecuta el caso de uso de almacenamiento temporal con la ruta de la presunción.
        /// </summary>
        /// <param name="context">Contexto del evento con la ruta de la presunción.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Consume(ConsumeContext<PresuncionUploadedOnSourceEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionSourcePath);
        }
    }
}
