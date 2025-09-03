using Renombrado.Fuentes.Domain.Contracts;
using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Domain.Services;
using Renombrado.Fuentes.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Renombrado.Fuentes.Application.UpdateFuenteFtp;

public class UpdateFuenteFtpUseCase
{
    private readonly IFuenteRepository _fuenteRepository;
    private readonly IEventBus _eventBus;
    private readonly FuenteFinder _fuenteFinder;
    public UpdateFuenteFtpUseCase(FuenteFinder fuenteFinder, IFuenteRepository fuenteRepository, IEventBus eventBus)
    {
        _fuenteRepository = fuenteRepository;
        _eventBus = eventBus;
        _fuenteFinder = fuenteFinder;
    }

    public async Task Execute(UpdateFuenteFtpDTO request)
    {
        var fuente = await _fuenteFinder.FindOrFail(new FuenteId(request.Id));
        EnsureFuenteIsFtp(fuente);
        ApplyUpdate((FuenteFtp)fuente, request);
        await _fuenteRepository.Update(fuente);
        await _eventBus.Publish(fuente.PullDomainEvents());
    }

    private void ApplyUpdate(FuenteFtp fuente, UpdateFuenteFtpDTO request)
    {
        fuente.Update(
            request.Descripcion.HasValue ? request.Descripcion.Value : fuente.Descripcion?.Value,
            request.Nombre.HasValue ? request.Nombre.Value : fuente.Nombre.Value,
            request.Host.HasValue ? request.Host.Value : fuente.Host.Value,
            request.Port.HasValue ? request.Port.Value : fuente.Port.Value,
            request.Username.HasValue ? request.Username.Value : fuente.Username.Value,
            request.Password.HasValue ? request.Password.Value : fuente.Password.Value
        );
    }

    private void EnsureFuenteIsFtp(Fuente fuente)
    {
        if (fuente is not FuenteFtp)
        {
            throw new InvalidOperationException("Fuente no es de tipo FTP");
        }
    }
}