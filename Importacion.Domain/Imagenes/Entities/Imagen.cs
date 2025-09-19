using System.Collections.Generic;
using System.IO;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using Infracsoft.Importacion.Domain.Imagenes.Events;
using Infracsoft.Importacion.Domain.Imagenes.ValueObjects;
using SharedKernel.Domain.Entities;

namespace Infracsoft.Importacion.Domain.Imagenes.Entities;

public class Imagen : AggregateRoot
{
    public ImagenId Id { get; }
    public ImagenRuta Ruta { get; private set; }
    public ImagenPeso Peso { get; private set; }
    public ImagenNombre Nombre { get; private set; }
    public PresuncionId PresuncionId { get; private set; }
    public string RutaCompleta { 
        get {
            return Path.Combine(Ruta.Value, Nombre.Value);
        }
    }

    private Imagen(string id, string ruta, float peso, string nombre, string presuncionId)
    {
        Id = new ImagenId(id);
        Ruta = new ImagenRuta(ruta);
        Peso = new ImagenPeso(peso);
        Nombre = new ImagenNombre(nombre);
        PresuncionId = new PresuncionId(presuncionId);
    }


    public static Imagen Create(string id, string ruta, float peso, string nombre, string presuncionId)
    {
        var imagen = new Imagen(id, ruta, peso, nombre, presuncionId);
        imagen.RecordDomainEvent(
            new ImagenCreatedDomainEvent{
                ImagenId = id,
                Ruta = ruta,
                Peso = peso,
                Nombre = nombre,
                PresuncionId = presuncionId
            });
        return imagen;
    }

    public void Delete()
    {
        RecordDomainEvent(
            new ImagenDeletedDomainEvent{
                ImagenId = Id.Value,
                Ruta = Ruta.Value,
                Peso = Peso.Value,
                Nombre = Nombre.Value,
                PresuncionId = PresuncionId.Value
            });
    }
}
