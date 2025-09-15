using Microsoft.AspNetCore.Mvc;
using RenombradoOld.API.Modules.Fuentes.DTO;
using RenombradoOld.Fuentes.Application.UpdateFuenteFtp;

namespace RenombradoOld.API.Modules.Fuentes.Handlers
{
    [ApiController]
    [Route("fuentes")]
    public class UpdateFuenteFtpHandler(UpdateFuenteFtpUseCase useCase) : ControllerBase
    {
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] UpdateFuenteRequest data)
        {
            await useCase.Execute(new UpdateFuenteFtpDTO(
                id.ToString(),
                data.Nombre,
                data.Descripcion,
                data.Host,
                data.Puerto,
                data.Usuario,
                data.Password
            ));
            return Ok();
        }
    }
}
