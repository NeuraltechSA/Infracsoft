namespace Infracsoft.Importacion.Domain.Imagenes.Contracts;


public interface IImagenStore
{
    Task Upload(Stream stream, string path);
    Task Delete(string imagePath);
}