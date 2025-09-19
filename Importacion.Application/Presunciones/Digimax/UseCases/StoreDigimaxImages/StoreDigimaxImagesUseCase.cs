using Infracsoft.Importacion.Domain.Presunciones.Services;

namespace Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages
{
    public sealed class StoreDigimaxImagesUseCase(
        PresuncionDigimaxImagenStore presuncionDigimaxImagenStore
    )
    {
        private readonly PresuncionDigimaxImagenStore _presuncionDigimaxImagenStore = presuncionDigimaxImagenStore;


        public async Task Execute(string basePath, string presuncionId)
        {
            await _presuncionDigimaxImagenStore.StoreImages(basePath, presuncionId);
        }
    }
}
