using RenombradoOld.Fuentes.Application.CreateFuenteFtp;
using RenombradoOld.Fuentes.Domain.Contracts;
using RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Repositories;
using RenombradoOld.API.Modules.Fuentes.Validators;
using FluentValidation;
using RenombradoOld.API.Modules.Fuentes.DTO;
using RenombradoOld.Fuentes.Domain.Services;
using RenombradoOld.Fuentes.Application.DeleteFuente;
using RenombradoOld.Fuentes.Application.UpdateFuenteFtp;

namespace RenombradoOld.API.Modules.Fuentes.Extensions
{
    public static class FuentesExtensions
    {
        public static IServiceCollection UseFuentesModule(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IFuenteRepository, FuenteRepository>();
            services.AddScoped<FuenteFinder>();
            #endregion

            #region UseCases
            services.AddScoped<UpdateFuenteFtpUseCase>();
            services.AddScoped<CreateFuenteFtpUseCase>();
            services.AddScoped<DeleteFuenteUseCase>();
            #endregion

            #region Validators
            services.AddScoped<IValidator<CreateFuenteRequest>, CreateFuenteFtpValidator>();
            services.AddScoped<IValidator<UpdateFuenteRequest>, UpdateFuenteFtpValidator>();
            #endregion

            return services;
        }
    }
}
