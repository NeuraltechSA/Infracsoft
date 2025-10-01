using Infracsoft.Importacion.Domain.UnidadesProduccion.Contracts;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Criteria;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Services;

public class UnidadProduccionUniquenessChecker
{
    private readonly IUnidadProduccionRepository _repository;

    public UnidadProduccionUniquenessChecker(IUnidadProduccionRepository repository)
    {
        _repository = repository;
    }

    public async Task EnsureNombreIsUnique(string nombre)
    {
        var criteria = UnidadProduccionCriteria.Create().WithNombre(nombre);
        var existingUnidades = await _repository.Find(criteria);
        
        EnsureNombreDoesNotExist(existingUnidades, nombre);
    }

    private void EnsureNombreDoesNotExist(IEnumerable<Entities.UnidadProduccion> unidades, string nombre)
    {
        if (unidades.Any())
        {
            throw UnidadProduccionNombreAlreadyExistsException.Create(nombre);
        }
    }
}
