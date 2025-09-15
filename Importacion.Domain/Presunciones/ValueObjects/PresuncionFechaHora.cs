using SharedKernel.Domain.ValueObjects;
using Infracsoft.Importacion.Domain.Presunciones.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.ValueObjects;

public sealed record PresuncionFechaHora : DateTimeValueObject
{
    public PresuncionFechaHora(DateTime value) : base(value)
    {
        EnsureNotOlderThanOneYear(value);
    }

    private static void EnsureNotOlderThanOneYear(DateTime value)
    {
        var unAnoAtras = DateTime.Now.AddYears(-1);
        if (value < unAnoAtras)
        {
            throw new InvalidPresuncionFechaHoraException($"La fecha y hora debe ser mayor o igual a hace un año ({unAnoAtras:yyyy-MM-dd HH:mm:ss})");
        }
    }
}