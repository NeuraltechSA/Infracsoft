using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using SharedKernel.Domain.Contracts;
using SharedKernel.Domain.Utilities;

namespace Infracsoft.Importacion.Domain.Presunciones.Services
{
    /// <summary>
    /// Servicio de dominio para el almacenamiento de archivos de presunción.
    /// Este servicio se encarga de procesar y almacenar las imágenes asociadas
    /// a una presunción en el sistema de archivos permanente.
    /// </summary>
    public class PresuncionFileStore(
        ImagenStore imagenStore,
        IGuidGenerator guidGenerator
    )
    {
        private readonly ImagenStore _imagenStore = imagenStore;
        private readonly IGuidGenerator _guidGenerator = guidGenerator;

        /// <summary>
        /// Almacena las imágenes de una presunción en el sistema de archivos permanente.
        /// Procesa cada imagen del stream asíncrono, genera un ID único para cada una
        /// y las almacena utilizando el servicio de almacenamiento de imágenes.
        /// </summary>
        /// <param name="images">Stream asíncrono de imágenes con nombre a almacenar.</param>
        /// <param name="presuncionId">ID único de la presunción a la que pertenecen las imágenes.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task StoreImages(IAsyncEnumerable<NamedStream> images, string presuncionId)
        {
            await foreach (var imageStream in images)
            {
                using var stream = imageStream;
                var imagenId = _guidGenerator.GenerateGuid().ToString();
                await _imagenStore.Store(imagenId, presuncionId, imageStream);
            }
        }
    }
}
