using RenombradoOld.Fuentes.Domain.Contracts;
using RenombradoOld.Fuentes.Domain.Services;
using RenombradoOld.Fuentes.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;


namespace RenombradoOld.Fuentes.Application.DeleteFuente;

public class DeleteFuenteUseCase
{
    private readonly IEventBus _eventBus;
    private readonly FuenteFinder _fuenteFinder;
    private readonly IFuenteRepository _fuenteRepository;
    public DeleteFuenteUseCase(FuenteFinder fuenteFinder,IFuenteRepository fuenteRepository, IEventBus eventBus)
    {
        _fuenteFinder = fuenteFinder;
        _eventBus = eventBus;
        _fuenteRepository = fuenteRepository;
    }

    public async Task Execute(string id)
    {
        var fuente = await _fuenteFinder.FindOrFail(new FuenteId(id));
        fuente.Delete();
        await _fuenteRepository.Delete(fuente);
        await _eventBus.Publish(fuente.PullDomainEvents());
    }

}