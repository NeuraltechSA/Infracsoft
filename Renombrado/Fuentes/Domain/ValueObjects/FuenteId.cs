using System;
using SharedKernel.Domain.ValueObjects;

namespace Renombrado.Fuentes.Domain.ValueObjects;

public sealed record FuenteId : UuidValueObject
{
    public FuenteId(string value) : base(value) { }

}