using FluentValidation;
using RenombradoOld.API.Modules.Fuentes.DTO;

namespace RenombradoOld.API.Modules.Fuentes.Validators;

public class UpdateFuenteFtpValidator : AbstractValidator<UpdateFuenteRequest>
{
    public UpdateFuenteFtpValidator()
    {
        When(x => x.Nombre.HasValue, () => {
            RuleFor(x => x.Nombre.Value).NotEmpty().MaximumLength(120);
        });
        When(x => x.Descripcion.HasValue, () => {
            RuleFor(x => x.Descripcion.Value).MaximumLength(500);
        });
        When(x => x.Host.HasValue, () => {
            RuleFor(x => x.Host.Value).NotEmpty().MaximumLength(100).Matches(@"^(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}|localhost|(\d{1,3}\.){3}\d{1,3})$");
        });
        When(x => x.Puerto.HasValue, () => {
            RuleFor(x => x.Puerto.Value).GreaterThan(0).LessThan(65536);
        });
        When(x => x.Usuario.HasValue, () => {
            RuleFor(x => x.Usuario.Value).NotEmpty().MaximumLength(100);
        });
        When(x => x.Password.HasValue, () => {
            RuleFor(x => x.Password.Value).NotEmpty().MaximumLength(100);
        });
        
    }
}