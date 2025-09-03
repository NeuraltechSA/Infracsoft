using Renombrado.Fuentes.Domain.Criteria;
using Renombrado.Fuentes.Domain.Entities;
using Renombrado.Fuentes.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;


namespace Renombrado.Fuentes.Domain.Contracts
{
    public interface IFuenteRepository : IRepository<Fuente, FuenteId, FuenteCriteria>
    {
    }
}
