namespace Renombrado.Fuentes.Domain.Exceptions;

/// <summary>
/// Exception thrown when a fuente is not found.
/// </summary>
class FuenteNotFoundException : Exception
{
    public FuenteNotFoundException(string id) : base($"Fuente no encontrada: {id}")
    {
    }
}