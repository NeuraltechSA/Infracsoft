using Infracsoft.Importacion.Application.Presunciones.Digimax.Infrastructure;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.CheckSourceDigimax;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.ImportPresuncionDigmax;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxTempFile;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.Presunciones.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Worker.Presunciones
{
    public static class PresuncionesExtensions
    {
        public static IServiceCollection UsePresuncionesModule(this IServiceCollection services)
        {
            #region  Contracts
            services.AddScoped<IGuidGenerator, GuidGenerator>();
            services.AddScoped<IPresuncionFileSource, SftpFileSource>();
            services.AddScoped<IPresuncionTempStore, LocalTempStore>();
            services.AddScoped<IPresuncionRepository, PresuncionRepository>();
            #endregion

            #region  Use cases
            services.AddScoped<CheckSourceDigimaxUseCase>();
            services.AddScoped<ImportPresuncionDigimaxUseCase>();
            services.AddScoped<StoreDigimaxTempFileUseCase>();
            services.AddScoped<DecompressDigimaxFileUseCase>();
            services.AddScoped<StoreDigimaxImagesUseCase>();
            #endregion

            #region Services
            services.AddScoped<PresuncionDigimaxImagenStore>();
            #endregion

            #region Consumers
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ImportOnPresuncionDigimaxImagesStored>();
                x.AddConsumer<DecompressOnDigimaxTempFileStored>();
                x.AddConsumer<StoreTempFileOnPresuncionDigimaxUploaded>();
                x.AddConsumer<StoreImagesOnDecompressedDigimaxFile>();
            });
            #endregion

            return services;
        }
    }
}
