using Infracsoft.Importacion.Domain.Imagenes.Criteria;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Imagenes.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Domain.Imagenes.Contracts;

public interface IImagenRepository : IRepository<Imagen, ImagenId, ImagenCriteria>
{
}