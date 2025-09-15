using RenombradoOld.Fuentes.Domain.Contracts;
using RenombradoOld.Fuentes.Domain.Entities;
using RenombradoOld.Fuentes.Domain.Exceptions;
using RenombradoOld.Fuentes.Domain.ValueObjects;

namespace RenombradoOld.Fuentes.Domain.Services;

public class FuenteFinder
{
    private readonly IFuenteRepository _fuenteRepository;
    public FuenteFinder(IFuenteRepository fuenteRepository)
    {
        _fuenteRepository = fuenteRepository;
    }

    public async Task<Fuente> FindOrFail(FuenteId id)
    {
        var fuente = await _fuenteRepository.Find(id);
        EnsureFuenteExists(id, fuente);
        return fuente!;
    }

    private void EnsureFuenteExists(FuenteId id, Fuente? fuente)
    {
        if (fuente == null)
        {
            throw new FuenteNotFoundException(id.Value);
        }
    }
}