using FluentValidation;
using Renombrado.API.Modules.Fuentes.DTO;
namespace Renombrado.API.Modules.Fuentes.Validators;

public class CreateFuenteFtpValidator : AbstractValidator<CreateFuenteRequest>
{
    public CreateFuenteFtpValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Descripcion).MaximumLength(500);
        RuleFor(x => x.Host).NotEmpty().MaximumLength(100).Matches(@"^(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}|localhost|(\d{1,3}\.){3}\d{1,3})$");
        RuleFor(x => x.Puerto).GreaterThan(0).LessThan(65536);
        RuleFor(x => x.Usuario).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
    }
}
