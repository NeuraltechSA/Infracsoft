using Infracsoft.Importacion.Domain.UnidadesProduccion.Events;
using Infracsoft.Importacion.Domain.UnidadesProduccion.ValueObjects;
using SharedKernel.Domain.Entities;

namespace Infracsoft.Importacion.Domain.UnidadesProduccion.Entities;

public sealed class UnidadProduccion : AggregateRoot
{
    public UnidadProduccionId Id { get; }
    public UnidadProduccionNombre Nombre { get; private set; }

    private UnidadProduccion(string id, string nombre)
    {
        Id = new UnidadProduccionId(id);
        Nombre = new UnidadProduccionNombre(nombre);
    }

    public static UnidadProduccion Create(string id, string nombre)
    {
        var unidadProduccion = new UnidadProduccion(id, nombre);
        
        unidadProduccion.RecordDomainEvent(
            new UnidadProduccionCreatedDomainEvent
            {
                UnidadProduccionId = id,
                Nombre = nombre
            });
            
        return unidadProduccion;
    }

    public void Update(string nombre)
    {
        Nombre = new UnidadProduccionNombre(nombre);
        
        RecordDomainEvent(
            new UnidadProduccionUpdatedDomainEvent
            {
                UnidadProduccionId = Id.Value,
                Nombre = nombre
            });
    }

    public void Delete()
    {
        RecordDomainEvent(
            new UnidadProduccionDeletedDomainEvent
            {
                UnidadProduccionId = Id.Value,
                Nombre = Nombre.Value
            });
    }
}
