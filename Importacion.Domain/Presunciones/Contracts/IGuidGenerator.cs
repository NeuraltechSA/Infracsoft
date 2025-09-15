using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{
    public interface IGuidGenerator
    {
        public Guid GenerateGuid();
    }
}
