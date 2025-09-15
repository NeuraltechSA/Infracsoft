namespace Infracsoft.Importacion.Domain.Imagenes.Contracts;


public interface IImageFileSource
{
    Task<string> Upload(Stream stream, string path);
    Task Delete(string imagePath);
}