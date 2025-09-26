
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.Imagenes.Services;

namespace Infracsoft.Importacion.Worker.Imagenes
{
    public static class ImagenesExtensions
    {
        public static IServiceCollection UseImagenesModule(this IServiceCollection services)
        {
            #region  Services
            services.AddScoped<ImagenStorageService>();
            services.AddScoped<IImagenRepository, ImagenRepository>();
            services.AddScoped<IImagenStore, AzureBlobStore>();
            #endregion

            #region  Use cases
            #endregion

            #region Consumers

            #endregion

            return services;
        }
    }
}
