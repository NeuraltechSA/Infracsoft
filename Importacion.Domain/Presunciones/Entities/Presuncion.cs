using System.Collections.Generic;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using SharedKernel.Domain.Entities;

namespace Infracsoft.Importacion.Domain.Presunciones.Entities;

public abstract class Presuncion : AggregateRoot
{
    public PresuncionId Id { get; }
    public PresuncionFechaHora? FechaHora { get; protected set; }
    public PresuncionLugar? Lugar { get; protected set; }
    public PresuncionPatente? Patente { get; protected set; }
    public ICollection<Imagen> Imagenes { get; private set; } = new List<Imagen>();

    protected Presuncion(string id,string? lugar, string? patente = null, DateTime ? fechaHora = null)
    {
        Id = new PresuncionId(id);
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = lugar != null ? new PresuncionLugar(lugar) : null;
        Patente = patente != null ? new PresuncionPatente(patente) : null;
    }

    public void Update(DateTime? fechaHora, string lugar, string patente)
    {
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = new PresuncionLugar(lugar);
        Patente = new PresuncionPatente(patente);
        //TODO: Update event
    }

    public void AddImagen(Imagen imagen)
    {
        Imagenes.Add(imagen);
    }

    public abstract void Delete();

}
