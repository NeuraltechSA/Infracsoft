using RenombradoOld.Presunciones.Domain.Contracts;
using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Domain.Exceptions;
using RenombradoOld.Presunciones.Domain.ValueObjects;

namespace RenombradoOld.Presunciones.Domain.Services;

public sealed class PresuncionFinder
{
    private readonly IPresuncionRepository _repository;

    public PresuncionFinder(IPresuncionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Presuncion> FindByIdAsync(PresuncionId id)
    {
        var presuncion = await _repository.Find(id);
        if (presuncion is null)
        {
            throw new PresuncionNotFoundException(id.Value);
        }
        return presuncion;
    }
}