using Infracsoft.Importacion.Domain.Presunciones.Entities;
using SharedKernel.Domain.Exceptions;

namespace Infracsoft.Importacion.Domain.Presunciones.Exceptions;

/// <summary>
/// Excepción que se lanza cuando el número de imágenes de una presunción Digimax no cumple con los requisitos.
/// Los radares Digimax deben capturar exactamente 2 imágenes por presunción.
/// </summary>
public class InvalidDigimaxImageCountException : DomainException
{
    /// <summary>
    /// Inicializa una nueva instancia de la excepción con el mensaje especificado.
    /// </summary>
    /// <param name="message">Mensaje de error</param>
    public InvalidDigimaxImageCountException(string message) : base(message)
    {
    }


    /// <summary>
    /// Crea una excepción específica para cuando se encuentra un número incorrecto de imágenes.
    /// </summary>
    /// <param name="actualCount">Número real de imágenes encontradas</param>
    /// <returns>Excepción configurada con mensaje específico</returns>
    public static InvalidDigimaxImageCountException Create(int actualCount)
    {
        return new InvalidDigimaxImageCountException(
            $"Los radares Digimax deben capturar exactamente {PresuncionVelocidad.DigimaxImageCount} imágenes por presunción. Se encontraron {actualCount} imágenes.");
    }
}



