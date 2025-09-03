using Renombrado.Presunciones.Domain.Contracts;
using Renombrado.Presunciones.Domain.Entities;
using Renombrado.Presunciones.Domain.Exceptions;
using Renombrado.Presunciones.Domain.ValueObjects;

namespace Renombrado.Presunciones.Domain.Services;

public sealed class PresuncionFinder
{
    private readonly IPresuncionRepository _repository;

    public PresuncionFinder(IPresuncionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Presuncion> FindByIdAsync(PresuncionId id)
    {
        var presuncion = await _repository.FindByIdAsync(id);
        if (presuncion is null)
        {
            throw new PresuncionNotFoundException(id.Value);
        }
        return presuncion;
    }
}