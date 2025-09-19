using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.SharedKernel.Domain.Utilities
{
    public record CompressedEntry(string Path, Stream Content);
}
