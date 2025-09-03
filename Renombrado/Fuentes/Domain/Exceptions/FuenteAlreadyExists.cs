namespace Renombrado.Fuentes.Domain.Exceptions;

public class FuenteAlreadyExists : Exception
{
    public FuenteAlreadyExists(string id) : base($"Fuente con id {id} ya existe")
    {
    }
}
