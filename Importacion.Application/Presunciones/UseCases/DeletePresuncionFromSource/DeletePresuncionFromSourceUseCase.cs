using Infracsoft.Importacion.Domain.Presunciones.Contracts;
namespace Infracsoft.Importacion.Application.Presunciones.UseCases.DeletePresuncionFromSource;

public class DeletePresuncionFromSourceUseCase(IPresuncionSource fileSource)
{
    private readonly IPresuncionSource _fileSource = fileSource;

    public async Task Execute(string presuncionPath)
    {
        await _fileSource.DeletePresuncion(presuncionPath);
    }
}