using BackEnd_TrecoLista.Domain.DTOs.DispositivoToken;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/dispositivoToken")]
    public class DispositivoTokenController : ControllerBase
    {
        private readonly IDispositivoTokenService _dispositivoTokenService;

        public DispositivoTokenController(IDispositivoTokenService dispositivoTokenService)
        {
            _dispositivoTokenService = dispositivoTokenService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DispositivoTokenDto>> GetById(int id)
        {
            var dispositivoToken = await _dispositivoTokenService.GetByIdAsync(id);
            if (dispositivoToken == null)
            {
                return NotFound();
            }
            return Ok(dispositivoToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispositivoTokenDto>>> GetAll()
        {
            var dispositivoTokens = await _dispositivoTokenService.GetAllAsync();
            return Ok(dispositivoTokens);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdateDeviceToken([FromBody] DispositivoTokenCreateDto dispositivoTokenCreateDto)
        {
            await _dispositivoTokenService.AddOrUpdateDeviceTokenAsync(dispositivoTokenCreateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _dispositivoTokenService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
