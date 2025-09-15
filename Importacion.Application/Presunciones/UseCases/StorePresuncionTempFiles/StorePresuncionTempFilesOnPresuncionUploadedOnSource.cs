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
    public class StoreTempPresuncionFilesOnPresuncionUploadedOnSource(StoreTempPresuncionFilesUseCase useCase) : IConsumer<PresuncionUploadedOnSourceEvent>
    {
        private readonly StoreTempPresuncionFilesUseCase _useCase = useCase;
        public async Task Consume(ConsumeContext<PresuncionUploadedOnSourceEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionSourcePath);
        }
    }
}
