using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionFromSource
{
    public class DeletePresuncionFromSourceOnPresuncionFilesStored(
        DeletePresuncionFromSourceUseCase deleteUseCase) : IConsumer<PresuncionFilesStoredEvent>
    {
        private readonly DeletePresuncionFromSourceUseCase _deleteUseCase = deleteUseCase;
        public async Task Consume(ConsumeContext<PresuncionFilesStoredEvent> context)
        {
            await _deleteUseCase.Execute(context.Message.PresuncionSourceFile);
        }
    }
}