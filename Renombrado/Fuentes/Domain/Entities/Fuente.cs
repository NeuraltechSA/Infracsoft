using System.Collections.Generic;
using Renombrado.Fuentes.Domain.ValueObjects;
using SharedKernel.Domain.Entities;

namespace Renombrado.Fuentes.Domain.Entities;

public abstract class Fuente : AggregateRoot
{
    public FuenteId Id { get; }
    public FuenteDescripcion? Descripcion { get; protected set; }
    public FuenteNombre Nombre { get; protected set; }

    protected Fuente(string id, string? descripcion, string nombre)
    {
        Id = new FuenteId(id);
        Descripcion = descripcion != null ? new FuenteDescripcion(descripcion) : null;
        Nombre = new FuenteNombre(nombre);
    }

    public void Update(string nombre, string? descripcion)
    {
        Nombre = new FuenteNombre(nombre);
        Descripcion = new FuenteDescripcion(descripcion);
    }

    public abstract void Delete();

    /// <summary>
    /// Convierte la configuración de la fuente a un diccionario de primitivos.
    /// </summary>
    /// <example>
    /// {
    ///     "host": "localhost",
    ///     "port": 21,
    ///     "username": "user",
    ///     "password": "password"
    /// }
    /// </example>
    /// <returns>Un diccionario de primitivos</returns>
    public abstract Dictionary<string, object> ConfigToPrimitives();
}