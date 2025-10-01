using Infracsoft.Importacion.Domain.Equipos.Contracts;
using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Domain.Equipos.Services;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Equipos.UseCases.CreateEquipo;

/// <summary>
/// Caso de uso para crear un nuevo equipo en el sistema.
/// Valida la unicidad del nombre antes de crear el equipo.
/// </summary>
public class CreateEquipoUseCase(
    IEquipoRepository equipoRepository,
    IUnitOfWork unitOfWork,
    IEventBus eventBus
)
{
    private readonly IEquipoRepository _equipoRepository = equipoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEventBus _eventBus = eventBus;
    private readonly EquipoUniquenessChecker _uniquenessChecker = new EquipoUniquenessChecker(equipoRepository);

    /// <summary>
    /// Ejecuta la creación de un equipo con validación de unicidad del nombre.
    /// </summary>
    /// <param name="id">ID único del equipo a crear.</param>
    /// <param name="nombre">Nombre del equipo a crear.</param>
    /// <param name="unidadProduccionId">ID de la unidad de producción a la que pertenece el equipo.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    public async Task Execute(string id, string nombre, string unidadProduccionId)
    {
        await _uniquenessChecker.EnsureNombreIsUnique(nombre);

        var equipo = Equipo.Create(id, nombre, unidadProduccionId);

        await _equipoRepository.Create(equipo);
        await _eventBus.Publish(equipo.PullDomainEvents());
        await _unitOfWork.SaveChangesAsync();
    }
}
