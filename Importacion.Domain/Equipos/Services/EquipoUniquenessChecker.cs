using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Criteria;
using Infracsoft.Importacion.Domain.Equipos.Exceptions;

namespace Infracsoft.Importacion.Domain.Equipos.Services;

public class EquipoUniquenessChecker
{
    private readonly IEquipoRepository _repository;

    public EquipoUniquenessChecker(IEquipoRepository repository)
    {
        _repository = repository;
    }

    public async Task EnsureNombreIsUnique(string nombre)
    {
        var criteria = EquipoCriteria.Create().WithNombre(nombre).Paginate(1, 1);
        var existingEquipos = await _repository.Find(criteria);
        
        EnsureNombreDoesNotExist(existingEquipos, nombre);
    }

    private void EnsureNombreDoesNotExist(IEnumerable<Entities.Equipo> equipos, string nombre)
    {
        if (equipos.Any())
        {
            throw EquipoNombreAlreadyExistsException.Create(nombre);
        }
    }
}
