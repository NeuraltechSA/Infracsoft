using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.Contracts;

namespace Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Services;

public class ImagenModelParser : IModelParser<Imagen, ImagenModel>
{
    public Imagen ParseToEntity(ImagenModel model)
    {
        return Imagen.Create(
            model.Id.ToString(),
            model.Ruta,
            model.Peso,
            model.Nombre,
            model.PresuncionId.ToString()
        );
    }

    public ImagenModel ParseToModel(Imagen entity)
    {
        return new ImagenModel
        {
            Id = new Guid(entity.Id.Value),
            Ruta = entity.Ruta.Value,
            Peso = entity.Peso.Value,
            Nombre = entity.Nombre.Value,
            PresuncionId = entity.PresuncionId?.Value != null ?  new Guid(entity.PresuncionId.Value) : null
        };
    }
}
