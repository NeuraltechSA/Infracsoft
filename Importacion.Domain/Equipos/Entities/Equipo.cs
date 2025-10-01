using Infracsoft.Importacion.Domain.Equipos.Events;
using Infracsoft.Importacion.Domain.Equipos.ValueObjects;
using SharedKernel.Domain.Entities;

namespace Infracsoft.Importacion.Domain.Equipos.Entities;

public sealed class Equipo : AggregateRoot
{
    public EquipoId Id { get; }
    public EquipoNombre Nombre { get; private set; }
    public EquipoUnidadProduccionId UnidadProduccionId { get; private set; }

    private Equipo(string id, string nombre, string unidadProduccionId)
    {
        Id = new EquipoId(id);
        Nombre = new EquipoNombre(nombre);
        UnidadProduccionId = new EquipoUnidadProduccionId(unidadProduccionId);
    }

    public static Equipo Create(string id, string nombre, string unidadProduccionId)
    {
        var equipo = new Equipo(id, nombre, unidadProduccionId);
        
        equipo.RecordDomainEvent(
            new EquipoCreatedDomainEvent
            {
                EquipoId = id,
                Nombre = nombre,
                UnidadProduccionId = unidadProduccionId
            });
            
        return equipo;
    }

    public void Update(string nombre, string unidadProduccionId)
    {
        Nombre = new EquipoNombre(nombre);
        UnidadProduccionId = new EquipoUnidadProduccionId(unidadProduccionId);
        
        RecordDomainEvent(
            new EquipoUpdatedDomainEvent
            {
                EquipoId = Id.Value,
                Nombre = nombre,
                UnidadProduccionId = unidadProduccionId
            });
    }

    public void Delete()
    {
        RecordDomainEvent(
            new EquipoDeletedDomainEvent
            {
                EquipoId = Id.Value,
                Nombre = Nombre.Value,
                UnidadProduccionId = UnidadProduccionId.Value
            });
    }
}
