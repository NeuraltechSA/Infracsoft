
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using SharedKernel.Domain.Contracts;
namespace Infracsoft.Importacion.Domain.Imagenes.Services
{
    public sealed class ImagenStore(
        IImagenStore store,
        IImagenRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        TimeProvider timeProvider
    )
    {
        private string GeneratePath(string id, string fileName)
        {
            return $"{timeProvider.GetUtcNow().ToString("yyyy/MM/dd")}/{id}{Path.GetExtension(fileName)}";
        }
        public async Task Store(string id, string presuncionId, string filename, Stream stream)
        {
            string? path = null;
            try
            {
                path = GeneratePath(id, filename);
                await store.Upload(stream, path);
                var imagen = Imagen.Create(id, path, stream.Length, filename, presuncionId);
                await repository.Create(imagen);
                await eventBus.Publish(imagen.PullDomainEvents());
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (path != null) await store.Delete(path);
                throw;
            }

            //TODO: Asegurarme que la operación sea atómica
        }
    }
}
