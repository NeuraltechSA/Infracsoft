
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

namespace Infracsoft.Importacion.Domain.Presunciones.Services;

/// <summary>
/// Servicio de dominio para la búsqueda de presunciones.
/// Este servicio encapsula la lógica de búsqueda de presunciones y maneja
/// las excepciones de dominio cuando no se encuentra una presunción.
/// </summary>
public sealed class PresuncionFinder
{
    private readonly IPresuncionRepository _repository;

    /// <summary>
    /// Inicializa una nueva instancia del servicio PresuncionFinder.
    /// </summary>
    /// <param name="repository">Repositorio de presunciones para realizar las consultas.</param>
    public PresuncionFinder(IPresuncionRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Busca una presunción por su ID único.
    /// Si no se encuentra la presunción, lanza una excepción de dominio.
    /// </summary>
    /// <param name="id">ID único de la presunción a buscar.</param>
    /// <returns>Task que contiene la presunción encontrada.</returns>
    /// <exception cref="PresuncionNotFoundException">Se lanza cuando no se encuentra la presunción con el ID especificado.</exception>
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