
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Imagenes.ValueObjects;
using SharedKernel.Domain.Contracts;
namespace Infracsoft.Importacion.Domain.Imagenes.Services
{
    public class ImagenStorageService(
        IImagenStore store,
        IImagenRepository repository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        TimeProvider timeProvider
    ) : IImagenStorageService
    {
        private string GeneratePath(string id, string fileName)
        {
            return $"{timeProvider.GetUtcNow().ToString("yyyy/MM/dd")}/{id}{Path.GetExtension(fileName)}";
        }
        public async Task Upload(string id,  string filename, Stream stream)
        {
            string? path = null;
            try
            {
                path = GeneratePath(id, filename);
                await store.Upload(stream, path);
                var imagen = Imagen.Create(id, path, stream.Length, filename);
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

        public async Task Delete(string id)
        {
            var imagen = await repository.Find(new ImagenId(id)); //TODO: Exception si no existe
            if (imagen != null)
            {
                await store.Delete(imagen.RutaCompleta);
                await repository.Delete(imagen);
                await eventBus.Publish(imagen.PullDomainEvents());
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
