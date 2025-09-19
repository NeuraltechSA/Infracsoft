using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infracsoft.Importacion.Infraestructure.Imagenes.Services
{
    public class AzureBlobStore : IImagenStore
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStore(
            BlobServiceClient blobServiceClient, 
            IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Delete(string imagePath)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("imagenes");
            var blobClient = containerClient.GetBlobClient(imagePath);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task Upload(Stream stream, string path)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("imagenes");

            // Asegurar que el contenedor existe
            await containerClient.CreateIfNotExistsAsync();

            //await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(path);

            // Subir el stream al blob
            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
}
