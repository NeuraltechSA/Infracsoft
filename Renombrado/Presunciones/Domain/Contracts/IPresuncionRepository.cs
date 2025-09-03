using Renombrado.Presunciones.Domain.Criteria;
using Renombrado.Presunciones.Domain.Entities;
using Renombrado.Presunciones.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Renombrado.Presunciones.Domain.Contracts;

public interface IPresuncionRepository : IRepository<Presuncion, PresuncionId, PresuncionCriteria>
{
}