using BackEnd_TrecoLista.Application.ViewModel;
using BackEnd_TrecoLista.Infraestrutura.Identity;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Auth([FromBody] AuthViewModel authViewModel)
        {
            var usuario = _usuarioRepository.GetAuth(authViewModel.Username, authViewModel.Password);

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
