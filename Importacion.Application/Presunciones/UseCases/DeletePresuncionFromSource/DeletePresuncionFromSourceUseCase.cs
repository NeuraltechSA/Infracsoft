using Infracsoft.Importacion.Domain.Presunciones.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionFromSource
{
    /// <summary>
    /// Caso de uso para eliminar una presunción de la fuente original.
    /// Este caso de uso se ejecuta después de que una presunción ha sido completamente
    /// procesada e importada, para limpiar la fuente y liberar espacio.
    /// </summary>
    public class DeletePresuncionFromSourceUseCase(IPresuncionSource fileSource)
    {
        private readonly IPresuncionSource _fileSource = fileSource;

        /// <summary>
        /// Ejecuta la eliminación de una presunción de la fuente original.
        /// Elimina todos los archivos de la presunción de la fuente especificada.
        /// </summary>
        /// <param name="presuncionSourcePath">Ruta de la presunción en la fuente a eliminar.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Execute(string presuncionSourcePath)
        {
            await _fileSource.DeletePresuncion(presuncionSourcePath);
        }
    }
}