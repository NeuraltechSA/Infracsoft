using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Criteria;
using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Imagenes.ValueObjects;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Services;
using SharedKernel.Infraestructure.Persistence.EFCore.Repositories;
using Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;

namespace Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Repositories;

public sealed class ImagenRepository : BaseEFCoreRepository<Imagen, ImagenId, ImagenModel, ImagenCriteria>, IImagenRepository
{
    public ImagenRepository(ImportacionDbContext context) 
        : base(context, new EFCoreCriteriaConverter(), new ImagenModelParser())
    {
    }
}
