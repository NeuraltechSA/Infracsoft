using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Domain.Utilities;
namespace Infracsoft.Importacion.Domain.Presunciones.Contracts
{
    /// <summary>
    /// Contrato para el almacenamiento temporal de archivos de presunción durante el proceso de importación.
    /// Proporciona operaciones para almacenar, recuperar y eliminar archivos de presunción de forma temporal.
    /// </summary>
    public interface IPresuncionTempStore
    {
        /// <summary>
        /// Almacena un archivo de presunción en el almacenamiento temporal.
        /// </summary>
        /// <param name="destinationPath">Ruta de destino donde almacenar el archivo.</param>
        /// <param name="stream">Stream con nombre que contiene los datos del archivo.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public Task Store(string destinationPath,NamedStream stream);

        /// <summary>
        /// Obtiene los datos raw de una presunción desde el almacenamiento temporal. Ejemplo: XML, JSON, etc.
        /// </summary>
        /// <param name="destinationPath">Ruta de destino donde están los archivos de la presunción.</param>
        /// <returns>Task que contiene los datos raw de la presunción como string.</returns>
        public Task<string> GetRawPresuncionData(string destinationPath);

        /// <summary>
        /// Obtiene todas las imágenes asociadas a una presunción desde el almacenamiento temporal.
        /// </summary>
        /// <param name="destinationPath">Ruta de destino donde están los archivos de la presunción.</param>
        /// <returns>AsyncEnumerable de NamedStream que contiene las imágenes de la presunción.</returns>
        public IAsyncEnumerable<NamedStream> GetPresuncionImages(string destinationPath);

        /// <summary>
        /// Obtiene todos los videos asociados a una presunción desde el almacenamiento temporal.
        /// </summary>
        /// <param name="destinationPath">Ruta de destino donde están los archivos de la presunción.</param>
        /// <returns>AsyncEnumerable de NamedStream que contiene los videos de la presunción.</returns>
        public IAsyncEnumerable<NamedStream> GetPresuncionVideos(string destinationPath);

        /// <summary>
        /// Elimina todos los archivos de una presunción del almacenamiento temporal.
        /// </summary>
        /// <param name="destinationPath">Ruta de destino donde están los archivos de la presunción.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public Task DeletePresuncion(string destinationPath);
    }
}
