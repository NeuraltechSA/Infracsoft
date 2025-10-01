using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Infracsoft.Importacion.Infraestructure.UnidadesProduccion.Persistence.EFCore.Models;

public class UnidadProduccionModel : BaseEFCoreModel
{
    [Required]
    [MaxLength(255)]
    public required string Nombre { get; init; }

    public UnidadProduccionModel() { }
}
