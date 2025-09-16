using Infracsoft.Importacion.Domain.Presunciones.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionTempFiles
{
    /// <summary>
    /// Caso de uso para eliminar archivos temporales de una presunción.
    /// Este caso de uso se utiliza tanto para limpieza normal como para compensación
    /// cuando falla el procesamiento de una presunción.
    /// </summary>
    public class DeletePresuncionTempFilesUseCase(IPresuncionTempStore tempStore)
    {
        private readonly IPresuncionTempStore _tempStore = tempStore;

        /// <summary>
        /// Ejecuta la eliminación de archivos temporales de una presunción.
        /// Elimina todos los archivos asociados a la ruta de destino especificada.
        /// </summary>
        /// <param name="presuncionDestinationPath">Ruta de destino a limpiar.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        public async Task Execute(string presuncionDestinationPath)
        {
            await _tempStore.DeletePresuncion(presuncionDestinationPath);
        }
    }
}
