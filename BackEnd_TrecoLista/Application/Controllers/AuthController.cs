using BackEnd_TrecoLista.Application.ViewModel;
using BackEnd_TrecoLista.Domain.DTOs.Usuario;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _usuarioService.AuthenticateAsync(usuarioLoginDto);

            if (usuario != null)
            {
                var token = TokenService.GenerateToken(usuario);
                return Ok(new { token });
            }
            else
            {
                return BadRequest("Usuário ou senha inválidos!");
            }
        }
    }
}
