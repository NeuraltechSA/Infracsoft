using System.Collections.Generic;
using Renombrado.Fuentes.Domain.Events;
using Renombrado.Fuentes.Domain.ValueObjects.Ftp;

namespace Renombrado.Fuentes.Domain.Entities;

public sealed class FuenteFtp : Fuente
{
    public FuenteFtpHost Host { get; private set; }
    public FuenteFtpPort Port { get; private set; }
    public FuenteFtpUsername Username { get; private set; }
    public FuenteFtpPassword Password { get; private set; }

    public FuenteFtp(
        string id,
        string nombre,
        string? descripcion,
        string host,
        int port,
        string username,
        string password) : base(id, descripcion, nombre)
    {
        Host = new FuenteFtpHost(host);
        Port = new FuenteFtpPort(port);
        Username = new FuenteFtpUsername(username);
        Password = new FuenteFtpPassword(password);
    }

    public static FuenteFtp Create(
        string id,
        string nombre,
        string? descripcion,
        string host,
        int port,
        string username,
        string password
    )
    {
        var fuente = new FuenteFtp(id, nombre, descripcion, host, port, username, password);
        fuente.RecordDomainEvent(
            new FuenteFtpCreatedDomainEvent{
                FuenteId = id,
                Nombre = nombre,
                Descripcion = descripcion,
                Host = host,
                Port = port,
                Username = username,
                Password = password
            });
        return fuente;
    }

    public void Update(
        string? descripcion, 
        string nombre, 
        string host, 
        int port, 
        string username, 
        string password
    )
    {
        Update(nombre, descripcion);
        Host = new FuenteFtpHost(host);
        Port = new FuenteFtpPort(port);
        Username = new FuenteFtpUsername(username);
        Password = new FuenteFtpPassword(password);
        RecordDomainEvent(
            new FuenteFtpUpdatedDomainEvent{
                FuenteId = Id.Value,
                Nombre = nombre,
                Descripcion = descripcion,
                Host = host,
                Port = port,
                Username = username,
                Password = password
            });
    }
    public override void Delete()
    {
        RecordDomainEvent(
            new FuenteFtpDeletedDomainEvent{
                FuenteId = Id.Value,
                Nombre = Nombre.Value,
                Descripcion = Descripcion?.Value,
                Host = Host.Value,
                Port = Port.Value,
                Username = Username.Value,
                Password = Password.Value
            });
    }

    public override Dictionary<string, object> ConfigToPrimitives()
    {
        return new Dictionary<string, object>
        {
            ["host"] = Host.Value,
            ["port"] = Port.Value,
            ["username"] = Username.Value,
            ["password"] = Password.Value
        };
    }

    
    /*
    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object>
        {
            ["id"] = Id.Value.ToString(),
            ["nombre"] = Nombre.Value,
            ["descripcion"] = Descripcion.Value,
            ["host"] = Host.Value,
            ["port"] = Port.Value,
            ["username"] = Username.Value,
            ["password"] = Password.Value
        };
    }*/
}