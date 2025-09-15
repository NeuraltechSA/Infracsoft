using Infracsoft.Importacion.Domain.Imagenes.Entities;
using Infracsoft.Importacion.Domain.Presunciones.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{
    public interface IPresuncionParser
    {
        public Task<Presuncion> ParsePresuncion(string id, string rawPresuncion);
    }
}
