namespace Infracsoft.Importacion.Domain.Presunciones.DTO;

/// <summary>
/// DTO que contiene los datos extraídos directamente del archivo Digimax.
/// </summary>
public record DigimaxDataDTO(
    DateTime FechaHora,
    string Lugar,
    float VelocidadMedida,
    float VelocidadMaxima,
    string CompressedFileSourcePath
);
