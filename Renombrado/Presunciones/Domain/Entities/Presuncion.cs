using System.Collections.Generic;
using Renombrado.Presunciones.Domain.ValueObjects;
using SharedKernel.Domain.Entities;

namespace Renombrado.Presunciones.Domain.Entities;

public abstract class Presuncion : AggregateRoot
{
    public PresuncionId Id { get; }
    public PresuncionFechaHora? FechaHora { get; protected set; }
    public PresuncionLugar Lugar { get; protected set; }
    public PresuncionPatente Patente { get; protected set; }

    protected Presuncion(string id, DateTime? fechaHora, string lugar, string patente)
    {
        Id = new PresuncionId(id);
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = new PresuncionLugar(lugar);
        Patente = new PresuncionPatente(patente);
    }

    public void Update(DateTime? fechaHora, string lugar, string patente)
    {
        FechaHora = fechaHora != null ? new PresuncionFechaHora(fechaHora.Value) : null;
        Lugar = new PresuncionLugar(lugar);
        Patente = new PresuncionPatente(patente);
    }

    public abstract void Delete();

    public abstract Dictionary<string, object> ToPrimitives();
}