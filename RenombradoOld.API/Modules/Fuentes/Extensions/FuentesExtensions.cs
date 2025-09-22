using RenombradoOld.API.Modules.Fuentes.Validators;
using FluentValidation;
using RenombradoOld.API.Modules.Fuentes.DTO;

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
