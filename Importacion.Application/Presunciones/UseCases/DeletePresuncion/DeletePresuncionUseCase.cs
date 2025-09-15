using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using Infracsoft.Importacion.Domain.Presunciones.ValueObjects;
using SharedKernel.Domain.Contracts;

namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncion
{
    /// <summary>
    /// Caso de uso para eliminar una presunción de la base de datos.
    /// Este caso de uso se utiliza principalmente para compensación cuando falla
    /// el almacenamiento de archivos de una presunción ya importada.
    /// </summary>
    public class DeletePresuncionUseCase(
        PresuncionFinder finder,
        IPresuncionRepository repository,
        IUnitOfWork unitOfWork
    )
    {
        private readonly IPresuncionRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Ejecuta la eliminación de una presunción de la base de datos.
        /// Busca la presunción por ID, la elimina del repositorio y persiste los cambios.
        /// </summary>
        /// <param name="presuncionId">ID único de la presunción a eliminar.</param>
        /// <returns>Task que representa la operación asíncrona.</returns>
        /// <exception cref="PresuncionNotFoundException">Se lanza cuando no se encuentra la presunción con el ID especificado.</exception>
        public async Task Execute(string presuncionId)
        {
            var presuncion = await finder.FindByIdAsync(new PresuncionId(presuncionId));
            await _repository.Delete(presuncion);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
