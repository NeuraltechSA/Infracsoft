using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.SharedKernel.Domain.Contracts;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile
{
    public class DecompressDigimaxFileUseCase(IDecompressor decompressor, IPresuncionTempStore tempStore, IEventBus eventBus)
    {
        private readonly IDecompressor _decompressor = decompressor;
        private readonly IPresuncionTempStore _tempStore = tempStore;
        private readonly IEventBus _eventBus = eventBus;

        public async Task Execute(string zipTempPath, string presuncionId)
        {
            using var stream = await _tempStore.DownloadFile(zipTempPath);
            var basePath = Path.GetFileNameWithoutExtension(zipTempPath);

            //TODO: pass from env
            await foreach(var entry in _decompressor.Decompress(stream, Path.GetFileName(zipTempPath), "SysDig302011"))
            {
                var path = Path.Combine(basePath, entry.Path);
                await _tempStore.Store(path, entry.Content);
            }
            await _eventBus.Publish(new DecompressedDigimaxFileEvent(basePath, presuncionId));
        }
    }
}
