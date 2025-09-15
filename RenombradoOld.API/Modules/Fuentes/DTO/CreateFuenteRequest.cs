using System.ComponentModel.DataAnnotations;
namespace RenombradoOld.API.Modules.Fuentes.DTO;

public record CreateFuenteRequest(
    string Nombre,
    string? Descripcion,
    string Usuario,
    string Password,
    string Host,
    int Puerto
);

