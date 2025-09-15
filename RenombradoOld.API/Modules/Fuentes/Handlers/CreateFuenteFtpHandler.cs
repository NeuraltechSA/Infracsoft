using Microsoft.AspNetCore.Mvc;
using RenombradoOld.API.Modules.Fuentes.DTO;
using RenombradoOld.Fuentes.Application.CreateFuenteFtp;

namespace RenombradoOld.API.Modules.Fuentes.Handlers
{
    [ApiController]
    [Route("fuentes")]
    public class CreateFuenteFtpHandler(CreateFuenteFtpUseCase useCase) : ControllerBase
    {
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Post(
            [FromRoute] Guid id, 
            [FromBody] CreateFuenteRequest data)
        {
            await useCase.Execute(new CreateFuenteFtpDTO(
                id.ToString(),
                data.Nombre,
                data.Descripcion,
                data.Host,
                data.Puerto,
                data.Usuario,
                data.Password
            ));
            return Created();
        }
    }
}
