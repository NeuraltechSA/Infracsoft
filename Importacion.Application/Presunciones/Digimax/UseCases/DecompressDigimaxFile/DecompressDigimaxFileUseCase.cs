using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.SharedKernel.Domain.Contracts;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile
{
    public sealed class DecompressDigimaxFileUseCase(
        IDecompressor decompressor, 
        IPresuncionTempStore tempStore,
        IEventBus eventBus,
        IUnitOfWork unitOfWork,
        IConfiguration configuration
    )
    {
        private readonly IDecompressor _decompressor = decompressor;
        private readonly IPresuncionTempStore _tempStore = tempStore;
        private readonly IEventBus _eventBus = eventBus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public async Task Execute(string tempFilePath, string originalSourcePath)
        {
            try
            {
                await Decompress(tempFilePath, originalSourcePath);
            }
            catch (Exception)
            {
                await _eventBus.Publish(new DigimaxDecompressionFailedEvent(tempFilePath));
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task Decompress(string tempFilePath, string originalSourcePath)
        {
            using var stream = await _tempStore.DownloadFile(tempFilePath);
            var tempBasePath = Path.GetFileNameWithoutExtension(tempFilePath);
            var password = _configuration["Digimax:FilePassword"];

            await foreach (var entry in _decompressor.Decompress(stream, Path.GetFileName(tempFilePath), password))
            {
                var path = Path.Combine(tempBasePath, entry.Path);
                await _tempStore.Store(path, entry.Content);
            }
            await _eventBus.Publish(new DecompressedDigimaxFileEvent(tempFilePath, tempBasePath, originalSourcePath));
        }
    }
}
