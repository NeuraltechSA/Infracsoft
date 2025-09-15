using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using System.ComponentModel.DataAnnotations;
using RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenombradoOld.Fuentes.Infrastructure.Persistence.EFCore.Models;

public class FuenteModel : BaseEFCoreModel
{
    [Required]
    [MaxLength(120)]
    public required string Nombre { get; init; }
    [MaxLength(500)]
    public required string? Descripcion { get; init; }
    [Required]
    public FuenteTipo Tipo { get; init; }
    [Required]
    [Column(TypeName = "jsonb")]
    public required string Config { get; init; }

}