using Infracsoft.Importacion.Domain.UnidadesProduccion.Contracts;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Exceptions;
using Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Services;

public class UnidadProduccionFinder
{
    private readonly IUnidadProduccionRepository _repository;

    public UnidadProduccionFinder(IUnidadProduccionRepository repository)
    {
        _repository = repository;
    }

    public async Task<UnidadProduccion> FindOrFail(UnidadProduccionId id)
    {
        var unidadProduccion = await _repository.Find(id);
        EnsureUnidadProduccionExists(unidadProduccion, id);
        return unidadProduccion!;
    }


    private void EnsureUnidadProduccionExists(UnidadProduccion? unidadProduccion, UnidadProduccionId id)
    {
        if (unidadProduccion == null)
        {
            throw UnidadProduccionNotExistsException.CreateFromId(id.Value);
        }
    }

}
