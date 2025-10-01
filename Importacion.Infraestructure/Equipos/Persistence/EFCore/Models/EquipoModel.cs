using Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Models;
using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Models;

public class EquipoModel : BaseEFCoreModel
{
    [Required]
    [MaxLength(255)]
    public required string Nombre { get; init; }
    
    [Required]
    public required Guid UnidadProduccionId { get; init; }
    public UnidadProduccionModel UnidadProduccion { get; set; } = null!;

    public EquipoModel() { }
}
