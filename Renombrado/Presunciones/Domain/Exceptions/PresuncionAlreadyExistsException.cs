using SharedKernel.Domain.Exceptions;

namespace Renombrado.Presunciones.Domain.Exceptions;

public sealed class PresuncionAlreadyExistsException : DomainException
{
    public PresuncionAlreadyExistsException(string presuncionId) 
        : base($"Presunción con ID {presuncionId} ya existe")
    {
    }
}