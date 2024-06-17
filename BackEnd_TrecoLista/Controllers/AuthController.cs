using BackEnd_TrecoLista.Repository.Interfaces;
using BackEnd_TrecoLista.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Controllers
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
        public IActionResult Auth(string username, string password)
        {
            var usuario =  _usuarioRepository.GetAuth(username, password);

            if (usuario != null)
            {
                var token = TokenService.GenerateToken(usuario);
                return Ok(token);
            }
            else 
            {
                return BadRequest("Usuário ou senha inválidos!");
            }
        }
    }
}
