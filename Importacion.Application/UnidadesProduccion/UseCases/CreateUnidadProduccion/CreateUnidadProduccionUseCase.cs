using Infracsoft.Importacion.Domain.UnidadesProduccion.Contracts;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;
using Infracsoft.Importacion.Domain.UnidadesProduccion.Services;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.UnidadesProduccion.UseCases.CreateUnidadProduccion;

/// <summary>
/// Caso de uso para crear una nueva unidad de producción en el sistema.
/// </summary>
public class CreateUnidadProduccionUseCase(
    IUnidadProduccionRepository unidadProduccionRepository,
    IUnitOfWork unitOfWork,
    IEventBus eventBus
)
{
    private readonly IUnidadProduccionRepository _unidadProduccionRepository = unidadProduccionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEventBus _eventBus = eventBus;
    private readonly UnidadProduccionUniquenessChecker _uniquenessChecker = new UnidadProduccionUniquenessChecker(unidadProduccionRepository);

    /// <summary>
    /// Ejecuta la creación de una unidad de producción con validación de unicidad del nombre.
    /// </summary>
    /// <param name="id">ID único de la unidad de producción a crear.</param>
    /// <param name="nombre">Nombre de la unidad de producción a crear.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Execute(string id, string nombre)
    {
        await _uniquenessChecker.EnsureNombreIsUnique(nombre);

        var unidadProduccion = UnidadProduccion.Create(id, nombre);

        await _unidadProduccionRepository.Create(unidadProduccion);
        await _eventBus.Publish(unidadProduccion.PullDomainEvents());
        await _unitOfWork.SaveChangesAsync();
    }
}
