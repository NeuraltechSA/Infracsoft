using Infracsoft.Importacion.Domain.Imagenes.Entities;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Infracsoft.Importacion.Domain.Imagenes.Contracts
{
    public interface IImagenStorageService
    {
        public Task Upload(string id, string filename, Stream stream);
        public Task Delete(string id);
    }
}
