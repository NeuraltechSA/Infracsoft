using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Contracts
{
    public abstract record DomainEvent
    {
        public abstract string MessageName { get; }
    }
}
