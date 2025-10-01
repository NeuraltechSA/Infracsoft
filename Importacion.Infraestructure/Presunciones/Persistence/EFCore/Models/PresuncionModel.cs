using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Enums;
using System.ComponentModel.DataAnnotations;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Models;
using Infracsoft.Importacion.Domain.Equipos.Entities;
using Infracsoft.Importacion.Infraestructure.Equipos.Persistence.EFCore.Models;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;

public class PresuncionModel : BaseEFCoreModel
{
    [Required]
    public Guid EquipoId { get; init; }
    public EquipoModel Equipo { get; set; } = null!;
    public DateTime? FechaHora { get; init; }
    
    [MaxLength(200)]
    public string? Lugar { get; init; }
    
    [MaxLength(8)]
    public string? Patente { get; init; }
    
    [Required]
    public TipoPresuncion TipoInfraccion { get; init; }
    
    // Campos genéricos para cualquier tipo de medición
    public float? ValorMedido { get; init; }
    public float? ValorMaximo { get; init; }
    public int? Carril { get; init; }

    public ICollection<ImagenModel> Imagenes { get; } = new List<ImagenModel>();
}