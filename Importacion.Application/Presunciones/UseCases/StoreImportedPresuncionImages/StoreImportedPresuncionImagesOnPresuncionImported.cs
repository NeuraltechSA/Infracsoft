using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using Infracsoft.Importacion.Application.Presunciones.UseCases.StoreImportedPresuncionFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.StoreImportedPresuncionFiles
{
    public class StoreImportedPresuncionImagesOnPresuncionImported(
        StoreImportedPresuncionImagesUseCase useCase) : IConsumer<PresuncionImportedEvent>
    {
        private readonly StoreImportedPresuncionImagesUseCase _useCase = useCase;
        public async Task Consume(ConsumeContext<PresuncionImportedEvent> context)
        {
            await _useCase.Execute(context.Message.PresuncionSourcePath, context.Message.PresuncionId);
        }
    }
}