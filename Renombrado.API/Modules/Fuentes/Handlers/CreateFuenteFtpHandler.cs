using Microsoft.AspNetCore.Mvc;
using Renombrado.API.Modules.Fuentes.DTO;
using Renombrado.Fuentes.Application.CreateFuenteFtp;

namespace Renombrado.API.Modules.Fuentes.Handlers
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
