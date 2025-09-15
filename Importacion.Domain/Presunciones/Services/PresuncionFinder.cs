
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

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