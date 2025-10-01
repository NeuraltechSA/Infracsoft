using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Domain.Equipos.Exceptions;
using Infracsoft.Importacion.Domain.Equipos.ValueObjects;

namespace Infracsoft.Importacion.Domain.Equipos.Services;

public class EquipoFinder
{
    private readonly IEquipoRepository _repository;

    public EquipoFinder(IEquipoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Equipo> FindOrFail(EquipoId id)
    {
        var equipo = await _repository.Find(id);
        EnsureEquipoExists(equipo, id);
        return equipo!;
    }


    private void EnsureEquipoExists(Equipo? equipo, EquipoId id)
    {
        if (equipo == null)
        {
            throw EquipoNotExistsException.CreateFromId(id.Value);
        }
    }
}
