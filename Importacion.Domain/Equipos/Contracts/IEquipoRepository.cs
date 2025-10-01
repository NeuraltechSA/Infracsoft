using Infracsoft.Importacion.Domain.Equipos.Criteria;
using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Domain.Equipos.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Equipos.Contracts;

public interface IEquipoRepository : IRepository<Equipo, EquipoId, EquipoCriteria>
{
}
