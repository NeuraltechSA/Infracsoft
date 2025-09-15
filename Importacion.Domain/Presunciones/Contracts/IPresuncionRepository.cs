using Infracsoft.Importacion.Domain.Presunciones.Criteria;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Presunciones.Contracts;

public interface IPresuncionRepository : IRepository<Presuncion, PresuncionId, PresuncionCriteria>
{
}