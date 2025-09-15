using RenombradoOld.Fuentes.Domain.Criteria;
using RenombradoOld.Fuentes.Domain.Entities;
using RenombradoOld.Fuentes.Domain.ValueObjects;
using SharedKernel.Domain.Contracts;


namespace RenombradoOld.Fuentes.Domain.Contracts
{
    public interface IFuenteRepository : IRepository<Fuente, FuenteId, FuenteCriteria>
    {
    }
}
