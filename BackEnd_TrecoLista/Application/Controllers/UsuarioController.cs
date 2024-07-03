using BackEnd_TrecoLista.Domain.DTOs.Usuario;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        //[Authorize]
        //[HttpGet("getAll")]
        //public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        //{
        //    var usuarios = await _usuarioService.GetAllAsync();
        //    return Ok(usuarios);
        //}

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            var userIdValue = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userIdValue)) return Unauthorized();

            int userId;

            bool coverteUserId = int.TryParse(userIdValue, out userId);


            if (coverteUserId)
            {
                var usuario = await _usuarioService.GetByIdAsync(userId);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            else return BadRequest();
        }

        [Authorize]
        [HttpGet("update")]
        public async Task<ActionResult<UsuarioUpdateDto>> GetUsuarioUpdate()
        {
            var userIdValue = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userIdValue)) return Unauthorized();

            int userId;

            bool coverteUserId = int.TryParse(userIdValue, out userId);


            if (coverteUserId)
            {
                var usuario = await _usuarioService.GetByIdAsync(userId);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            var usuarioDto = await _usuarioService.AddAsync(usuarioCreateDto);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDto.Id }, usuarioDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutUsuario(UsuarioUpdateDto usuarioUpdateDto)
        {
            var userIdValue = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userIdValue)) return Unauthorized();

            int userId;

            bool coverteUserId = int.TryParse(userIdValue, out userId);

            if (coverteUserId)
            {
                await _usuarioService.UpdateAsync(userId, usuarioUpdateDto);
                return NoContent();
            }
            else return BadRequest(nameof(PostUsuario));
        }

        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsuario(int id)
        //{
        //    await _usuarioService.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
