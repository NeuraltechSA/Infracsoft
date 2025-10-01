using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Velocidad;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects.Velocidad;

namespace Infracsoft.Importacion.Domain.Presunciones.Entities;

public sealed class PresuncionVelocidad : Presuncion
{
    public static readonly int DigimaxImageCount = 2;
    public static readonly string DigimaxFilenameRegex = @"^(?<lugar>.*)#(?<fecha>.*)#(?<hora>.*)#(?<maxima>.*)#(?<medida>.*)(?:#(?<carril>.*))?\.zip$";
    public PresuncionVelocidadMedida VelocidadMedida { get; private set; }
    public PresuncionVelocidadMaxima VelocidadMaxima { get; private set; }
    public PresuncionCarril? Carril { get; private set; }

    public PresuncionVelocidad(
        string id,
        string equipoId,
        List<string> imagenes,
        float velocidadMedida,
        float velocidadMaxima,
        int? carril,
        DateTime? fechaHora,
        string? lugar,
        string? patente) : base(id, equipoId, imagenes, lugar, patente, fechaHora)
    {
        VelocidadMaxima = new PresuncionVelocidadMaxima(velocidadMaxima);
        VelocidadMedida = new PresuncionVelocidadMedida(velocidadMedida, velocidadMaxima);
        Carril = carril.HasValue ? new PresuncionCarril(carril.Value) : null;
    }

    public static PresuncionVelocidad Create(
        string id,
        string equipoId,
        List<string> imagenes,
        float velocidadMedida,
        float velocidadMaxima,
        int? carril,
        DateTime? fechaHora,
        string? lugar,
        string? patente
    )
    {
        var presuncion = new PresuncionVelocidad(id, equipoId, imagenes, velocidadMedida, velocidadMaxima, carril, fechaHora, lugar, patente);
        presuncion.RecordDomainEvent(
            new PresuncionVelocidadCreatedDomainEvent
            {
                PresuncionId = id,
                EquipoId = equipoId,
                FechaHora = fechaHora,
                Lugar = lugar,
                Patente = patente,
                VelocidadMedida = velocidadMedida,
                VelocidadMaxima = velocidadMaxima,
                Carril = carril
            });
        return presuncion;
    }

    public void Update(
        DateTime? fechaHora,
        string lugar,
        string patente,
        float velocidadMedida,
        float velocidadMaxima,
        int? carril
    )
    {
        Update(fechaHora, lugar, patente);
        VelocidadMaxima = new PresuncionVelocidadMaxima(velocidadMaxima);
        VelocidadMedida = new PresuncionVelocidadMedida(velocidadMedida, velocidadMaxima);
        Carril = carril.HasValue ? new PresuncionCarril(carril.Value) : null;
        RecordDomainEvent(
            new PresuncionVelocidadUpdatedDomainEvent
            {
                PresuncionId = Id.Value,
                EquipoId = EquipoId.Value,
                FechaHora = fechaHora,
                Lugar = lugar,
                Patente = patente,
                VelocidadMedida = velocidadMedida,
                VelocidadMaxima = velocidadMaxima,
                Carril = carril
            });
    }

    public static PresuncionVelocidad ImportFromDigimax(string id, string equipoId, List<string> imagenesIds, string compressedFileSourcePath)
    {
            EnsureValidDigimaxImageCount(imagenesIds);
            var match = EnsureValidDigimaxFilename(Path.GetFileName(compressedFileSourcePath));
            var lugar = match.Groups["lugar"].Value;
            var fecha = DateTime.ParseExact($"{match.Groups["fecha"].Value} {match.Groups["hora"].Value}", "dd-MM-yyyy HH.mm.ss", CultureInfo.InvariantCulture);
            var maxima = float.Parse(match.Groups["maxima"].Value);
            var medida = float.Parse(match.Groups["medida"].Value);            
            var presuncion = new PresuncionVelocidad(id, equipoId, imagenesIds, medida, maxima, null, fecha, lugar, null);
            
            presuncion.RecordDomainEvent(
                new PresuncionDigimaxImportedEvent
                {
                    PresuncionId = id,
                    EquipoId = equipoId,
                    CompressedFileSourcePath = compressedFileSourcePath,
                    FechaHora = fecha,
                    Lugar = lugar,
                    Patente = null,
                    VelocidadMedida = medida,
                    VelocidadMaxima = maxima,
                    Carril = null
                });

            return presuncion;
    }

    private static void EnsureValidDigimaxImageCount(List<string> imagenes)
    {
        if(imagenes.Count != DigimaxImageCount)
        {
            throw InvalidDigimaxImageCountException.Create(DigimaxImageCount);
        }
    }


    private static Match EnsureValidDigimaxFilename(string filename)
    {
        var match = Regex.Match(filename, DigimaxFilenameRegex);
        if (!match.Success)
        {
            //TODO: Custom exception
            throw new InvalidOperationException($"Invalid filename: {filename}");
        }
        return match;
    }

    public override void Delete()
    {
        RecordDomainEvent(
            new PresuncionVelocidadDeletedDomainEvent
            {
                PresuncionId = Id.Value,
                EquipoId = EquipoId.Value,
                FechaHora = FechaHora?.Value,
                Lugar = Lugar?.Value,
                Patente = Patente?.Value,
                VelocidadMedida = VelocidadMedida.Value,
                VelocidadMaxima = VelocidadMaxima.Value,
                Carril = Carril?.Value
            });
    }

}