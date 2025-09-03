using Renombrado.Fuentes.Domain.Contracts;
using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Domain.Exceptions;
using Renombrado.Fuentes.Domain.ValueObjects;

namespace Renombrado.Fuentes.Domain.Services;

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