
using SharedKernel.Application.DTO;
using SharedKernel.Infraestructure.Converters;
using System.Text.Json.Serialization;

namespace RenombradoOld.API.Modules.Fuentes.DTO
{
    public record UpdateFuenteRequest
    {
        [JsonConverter(typeof(OptionalConverter<string>))]
        public Optional<string> Nombre { get; init; }

        [JsonConverter(typeof(OptionalConverter<string?>))]
        public Optional<string?> Descripcion { get; init; }

        [JsonConverter(typeof(OptionalConverter<string>))]
        public Optional<string> Usuario { get; init; }

        [JsonConverter(typeof(OptionalConverter<string>))]
        public Optional<string> Password { get; init; }

        [JsonConverter(typeof(OptionalConverter<string>))]
        public Optional<string> Host { get; init; }

        [JsonConverter(typeof(OptionalConverter<int>))]
        public Optional<int> Puerto { get; init; }
    }
}
