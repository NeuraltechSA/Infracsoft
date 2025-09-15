namespace RenombradoOld.Fuentes.Application.CreateFuenteFtp;

public record CreateFuenteFtpDTO(
    string Id,
    string Nombre,
    string? Descripcion,
    string Host,
    int Port,
    string Username,
    string Password
);