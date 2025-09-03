using System.Collections.Generic;
using Renombrado.Fuentes.Domain.Events.Neuralsys;
using Renombrado.Fuentes.Domain.ValueObjects.Neuralsys;

namespace Renombrado.Fuentes.Domain.Entities;

public sealed class FuenteNeuralsys : Fuente
{
    public FuenteNeuralsysUrl Url { get; private set; }
    public FuenteNeuralsysUsuario Usuario { get; private set; }
    public FuenteNeuralsysContrasena Contrasena { get; private set; }

    public FuenteNeuralsys(
        string id,
        string nombre,
        string? descripcion,
        string url,
        string usuario,
        string contrasena) : base(id, descripcion, nombre)
    {
        Url = new FuenteNeuralsysUrl(url);
        Usuario = new FuenteNeuralsysUsuario(usuario);
        Contrasena = new FuenteNeuralsysContrasena(contrasena);
    }

    public static FuenteNeuralsys Create(
        string id,
        string nombre,
        string? descripcion,
        string url,
        string usuario,
        string contrasena
    )
    {
        var fuente = new FuenteNeuralsys(id, nombre, descripcion, url, usuario, contrasena);
        fuente.RecordDomainEvent(
            new FuenteNeuralsysCreatedDomainEvent{
                FuenteId = id,
                Nombre = nombre,
                Descripcion = descripcion,
                Url = url,
                Usuario = usuario,
                Contrasena = contrasena
            });
        return fuente;
    }

    public void Update(
        string? descripcion, 
        string nombre, 
        string url, 
        string usuario, 
        string contrasena
    )
    {
        Update(nombre, descripcion);
        Url = new FuenteNeuralsysUrl(url);
        Usuario = new FuenteNeuralsysUsuario(usuario);
        Contrasena = new FuenteNeuralsysContrasena(contrasena);
        RecordDomainEvent(
            new FuenteNeuralsysUpdatedDomainEvent{
                FuenteId = Id.Value,
                Nombre = nombre,
                Descripcion = descripcion,
                Url = url,
                Usuario = usuario,
                Contrasena = contrasena
            });
    }

    public override void Delete()
    {
        RecordDomainEvent(
            new FuenteNeuralsysDeletedDomainEvent{
                FuenteId = Id.Value,
                Nombre = Nombre.Value,
                Descripcion = Descripcion?.Value,
                Url = Url.Value,
                Usuario = Usuario.Value,
                Contrasena = Contrasena.Value
            });
    }

    public override Dictionary<string, object> ConfigToPrimitives()
    {
        return new Dictionary<string, object>
        {
            ["url"] = Url.Value,
            ["usuario"] = Usuario.Value,
            ["contrasena"] = Contrasena.Value
        };
    }
}