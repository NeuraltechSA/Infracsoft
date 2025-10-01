using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Domain.Equipos.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.DTO;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using SharedKernel.Domain.Contracts;
using System.Text.RegularExpressions;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.CheckSourceDigimax;

/// <summary>
/// Caso de uso para chequear la fuente de archivos Digimax y disparar eventos para procesar las presunciones.
/// Escanea recursivamente la fuente de archivos, filtra archivos Digimax válidos y los procesa automáticamente.
/// </summary>
public class CheckSourceDigimaxUseCase(
    IPresuncionFileSource fileSource,
    IPresuncionRepository repository,
    EquipoPathExtractor equipoExtractor,
    IEventBus eventBus,
    IUnitOfWork unitOfWork
)
{
    private readonly IPresuncionFileSource _fileSource = fileSource;
    private readonly IPresuncionRepository _repository = repository;
    private readonly EquipoPathExtractor _equipoExtractor = equipoExtractor;
    private readonly IEventBus _eventBus = eventBus;
    private readonly PresuncionDuplicateValidator _duplicateValidator = new PresuncionDuplicateValidator(repository);
    private readonly DigimaxDataExtractor _dataExtractor = new DigimaxDataExtractor();
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Escanea la fuente de archivos Digimax y procesa todos los archivos válidos encontrados.
    /// </summary>
    public async Task Execute()
    {
        //TODO: try catch, logging
        await CheckSource();
    }

    /// <summary>
    /// Lógica principal que escanea recursivamente la fuente, filtra archivos Digimax válidos y los procesa.
    /// Para cada archivo válido: extrae datos, valida equipo, verifica duplicados y dispara eventos.
    /// </summary>
    public async Task CheckSource()
    {
        var digimaxFiles = await GetDigimaxFiles();
        foreach (var path in digimaxFiles)
        {
            await ProcessFile(path);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<IEnumerable<string>> GetDigimaxFiles()
    {
        var filePaths = await _fileSource.GetAllFilePathsRecursive();
        return filePaths.Where(p => Regex.IsMatch(Path.GetFileName(p), PresuncionVelocidad.DigimaxFilenameRegex));
    }

    private async Task ProcessFile(string path)
    {
        try
        {
            var data = _dataExtractor.ExtractData(path);
            var equipo = await _equipoExtractor.ExtractEquipoFromPath(path);
            await _duplicateValidator.EnsureNoDuplicate(data.FechaHora, data.Lugar, equipo.Id.Value);
            await _eventBus.Publish(new PresuncionDigimaxUploadedEvent(path));
        }
        catch (PresuncionAlreadyExistsException)
        {
            await _eventBus.Publish(new DuplicatedPresuncionDigimaxUploadedEvent(path));
        }
    }

}