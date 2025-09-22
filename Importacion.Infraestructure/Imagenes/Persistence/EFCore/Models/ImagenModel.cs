using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Models;

public class ImagenModel : BaseEFCoreModel
{
    [Required]
    [MaxLength(500)]
    public required string Ruta { get; init; }
    
    [Required]
    public float Peso { get; init; }
    
    [Required]
    [MaxLength(255)]
    public required string Nombre { get; init; }
    
    public Guid? PresuncionId { get; init; }

    public ImagenModel() { }
}
