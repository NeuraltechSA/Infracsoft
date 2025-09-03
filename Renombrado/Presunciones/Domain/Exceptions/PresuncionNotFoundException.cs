using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class PresuncionNotFoundException : DomainException
{
    public PresuncionNotFoundException(string presuncionId) 
        : base($"Presunción con ID {presuncionId} no encontrada")
    {
    }
}