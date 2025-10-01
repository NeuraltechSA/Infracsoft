using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using SharedKernel.Domain.Entities;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace Infracsoft.Importacion.Domain.Presunciones.Entities;

public abstract class Presuncion : AggregateRoot
{
    public PresuncionId Id { get; }
    public PresuncionEquipoId EquipoId { get; }
    public PresuncionFechaHora? FechaHora { get; protected set; }
    public PresuncionLugar? Lugar { get; protected set; }
    public PresuncionPatente? Patente { get; protected set; }
    public PresuncionImagenes Imagenes { get; protected set; }

    protected Presuncion(string id, string equipoId, List<string> imagenes, string? lugar, string? patente = null, DateTime ? fechaHora = null)
    {
        Id = new PresuncionId(id);
        EquipoId = new PresuncionEquipoId(equipoId);
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = lugar != null ? new PresuncionLugar(lugar) : null;
        Patente = patente != null ? new PresuncionPatente(patente) : null;
        Imagenes = new PresuncionImagenes(imagenes.Select(id => new PresuncionImagenId(id)).ToList());
    }

    public void Update(DateTime? fechaHora, string lugar, string patente)
    {
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = new PresuncionLugar(lugar);
        Patente = new PresuncionPatente(patente);
        //TODO: Update event
    }


    public abstract void Delete();

}
