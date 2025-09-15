using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;

public class PresuncionModel : BaseEFCoreModel
{
    public DateTime? FechaHora { get; init; }
    
    [Required]
    [MaxLength(200)]
    public required string Lugar { get; init; }
    
    [Required]
    [MaxLength(8)]
    public required string Patente { get; init; }
    
    [Required]
    public TipoPresuncion TipoInfraccion { get; init; }
    
    // Campos genéricos para cualquier tipo de medición
    public float? ValorMedido { get; init; }
    public float? ValorMaximo { get; init; }
    public int? Carril { get; init; }
}