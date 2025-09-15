using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracsoft.Importacion.Infraestructure.Presunciones.Contracts
{
    /// <summary>
    /// Interfaz que define el contrato para descomprimir archivos ZIP
    /// </summary>
    public interface IUnzipper
    {
        /// <summary>
        /// Descomprime un archivo ZIP en una carpeta de destino específica
        /// </summary>
        /// <param name="zipFilePath">Ruta del archivo ZIP a descomprimir</param>
        /// <param name="destinationFolder">Carpeta de destino donde se extraerán los archivos</param>
        /// <param name="password">Contraseña opcional para archivos ZIP protegidos</param>
        /// <returns>Una tarea que representa la operación asíncrona de descompresión</returns>
        public Task Unzip(string zipFilePath, string destinationFolder, string? password = null);
    }
}
