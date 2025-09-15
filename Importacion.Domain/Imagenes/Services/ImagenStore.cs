using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using SharedKernel.Domain.Contracts;
using SharedKernel.Domain.Utilities;

namespace Infracsoft.Importacion.Domain.Imagenes.Services
{
    public sealed class ImagenStore(
        IImageFileSource fileSource,
        IImagenRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        TimeProvider timeProvider
    )
    {
        private string GeneratePath(string id, string fileName)
        {
            return $"{timeProvider.GetUtcNow().Year}{timeProvider.GetUtcNow().Month}{timeProvider.GetUtcNow().Day}/{id}.{Path.GetExtension(fileName)}";
        }
        public async Task Store(string id, string presuncionId, NamedStream namedStream)
        {
            string? path = null;
            try
            {
                path = GeneratePath(id, namedStream.Name);
                await fileSource.Upload(namedStream, path);
                var imagen = Imagen.Create(id, path, namedStream.Length, namedStream.Name, presuncionId);
                await repository.Create(imagen);
                await eventBus.Publish(imagen.PullDomainEvents());
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (path != null) await fileSource.Delete(path);
                throw;
            }

            //TODO: Asegurarme que la operación sea atómica
        }
    }
}
