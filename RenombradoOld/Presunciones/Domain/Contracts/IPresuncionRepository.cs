using RenombradoOld.Presunciones.Domain.Criteria;
using RenombradoOld.Presunciones.Domain.Entities;
using RenombradoOld.Presunciones.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace RenombradoOld.Presunciones.Domain.Contracts;

public interface IPresuncionRepository : IRepository<Presuncion, PresuncionId, PresuncionCriteria>
{
}