using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository.Interfaces;
using BackEnd_TrecoLista.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add(UsuarioViewModel usuarioViewModel)
        {
            var usuario = new Usuario(
                0,
                usuarioViewModel.Email,
                usuarioViewModel.Senha,
                usuarioViewModel.Login,
                usuarioViewModel.TipoUsuario
                );
            _usuarioRepository.Add(usuario);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuarioRepository.Get();

            return Ok(usuarios);
        }
    }
}
