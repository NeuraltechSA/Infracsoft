using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Criteria;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

/// <summary>
/// Servicio que valida que no existan presunciones duplicadas.
/// Una presunción se considera duplicada si existe otra con la misma fecha/hora, lugar y equipo.
/// </summary>
public class PresuncionDuplicateValidator
{
    private readonly IPresuncionRepository _repository;

    public PresuncionDuplicateValidator(IPresuncionRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Valida que no exista una presunción duplicada con la misma fecha/hora, lugar y equipo.
    /// </summary>
    /// <param name="fechaHora">Fecha y hora de la presunción a validar.</param>
    /// <param name="lugar">Lugar de la presunción a validar.</param>
    /// <param name="equipoId">ID del equipo de la presunción a validar.</param>
    /// <returns>Task que representa la operación asíncrona.</returns>
    /// <exception cref="PresuncionAlreadyExistsException">Se lanza cuando existe una presunción duplicada.</exception>
    public async Task EnsureNoDuplicate(DateTime fechaHora, string lugar, string equipoId)
    {
        var criteria = PresuncionCriteria.Create()
            .WithFechaHora(fechaHora)
            .WithLugar(lugar)
            .WithEquipoId(equipoId);
            
        var presunciones = await _repository.Find(criteria);
        
        EnsureNoDuplicateFound(presunciones, fechaHora, lugar, equipoId);
    }

    private void EnsureNoDuplicateFound(IEnumerable<Presuncion> presunciones, DateTime fechaHora, string lugar, string equipoId)
    {
        if (presunciones.Any())
        {
            throw PresuncionAlreadyExistsException.Create(fechaHora, lugar, equipoId);
        }
    }
}
