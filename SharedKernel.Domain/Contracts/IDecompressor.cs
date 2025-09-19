using Infracsoft.SharedKernel.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.SharedKernel.Domain.Contracts
{
    public interface IDecompressor
    {
        public IAsyncEnumerable<CompressedEntry> Decompress(Stream compressedFile, string filename, string? password = null);
    }
}
