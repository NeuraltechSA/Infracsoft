
using SharedKernel.Application.DTO;

namespace RenombradoOld.Fuentes.Application.UpdateFuenteFtp;

public record UpdateFuenteFtpDTO(
    string Id ,
    Optional<string> Nombre,
    Optional<string?> Descripcion,
    Optional<string> Host,
    Optional<int> Port,
    Optional<string> Username,
    Optional<string> Password
);