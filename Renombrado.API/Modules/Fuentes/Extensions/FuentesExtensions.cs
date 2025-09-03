using Renombrado.Fuentes.Application.CreateFuenteFtp;
using Renombrado.API.Modules.Fuentes;
using Renombrado.Fuentes.Domain.Contracts;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Repositories;
using Renombrado.API.Modules.Fuentes.Validators;
using FluentValidation;
using Renombrado.API.Modules.Fuentes.DTO;
using Renombrado.Fuentes.Domain.Services;
using Renombrado.Fuentes.Application.DeleteFuente;
using Renombrado.Fuentes.Application.UpdateFuenteFtp;

namespace Renombrado.API.Modules.Fuentes.Extensions
{
    public static class FuentesExtensions
    {
        public static IServiceCollection UseFuentesModule(this IServiceCollection services)
        {
            services.AddScoped<IFuenteRepository, FuenteRepository>();
            services.AddScoped<FuenteFinder>();
            services.AddScoped<UpdateFuenteFtpUseCase>();
            services.AddScoped<CreateFuenteFtpUseCase>();
            services.AddScoped<DeleteFuenteUseCase>();

            services.AddScoped<IValidator<CreateFuenteRequest>, CreateFuenteFtpValidator>();
            services.AddScoped<IValidator<UpdateFuenteRequest>, UpdateFuenteFtpValidator>();
            
            return services;
        }
    }
}
