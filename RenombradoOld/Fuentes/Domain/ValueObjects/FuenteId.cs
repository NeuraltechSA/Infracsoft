using System;
using SharedKernel.Domain.ValueObjects;

namespace RenombradoOld.Fuentes.Domain.ValueObjects;

public sealed record FuenteId : UuidValueObject
{
    public FuenteId(string value) : base(value) { }

}