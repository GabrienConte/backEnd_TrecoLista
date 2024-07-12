using BackEnd_TrecoLista.Domain.DTOs.Favorito;
using BackEnd_TrecoLista.Domain.Services;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/favorito")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;
        private readonly IUsuarioService _usuarioService;

        public FavoritoController(IFavoritoService favoritoService, IUsuarioService usuarioService)
        {
            _favoritoService = favoritoService;
            _usuarioService = usuarioService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<FavoritoDto>>> GetAll()
        //{
        //    var favoritos = await _favoritoService.GetAllAsync();
        //    return Ok(favoritos);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoritoDto>> GetById(int id)
        {
            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null) return NotFound();
            return Ok(favorito);
        }

        //[HttpPost]
        //public async Task<ActionResult<FavoritoDto>> Create(FavoritoCreateDto favoritoCreateDto)
        //{
        //    var favorito = await _favoritoService.AddAsync(favoritoCreateDto);
        //    return CreatedAtAction(nameof(GetById), new { id = favorito.Id }, favorito);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<FavoritoDto>> Update(int id, FavoritoUpdateDto favoritoUpdateDto)
        //{
        //    var favorito = await _favoritoService.UpdateAsync(id, favoritoUpdateDto);
        //    if (favorito == null) return NotFound();
        //    return Ok(favorito);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var success = await _favoritoService.DeleteAsync(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}

        [Authorize]
        [HttpPost("favoritar")]
        public async Task<ActionResult<FavoritoDto>> FavoritarProduto([FromBody] ProdutoFavoritoDTO fav)
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
                var createFavorito = new FavoritoCreateDto
                {
                    ProdutoId = fav.ProdutoId,
                    UsuarioId = usuario.Id,
                    Aviso = false,
                    Prioridade = 1,
                };
                var favorito = await _favoritoService.AddAsync(createFavorito);
                return NoContent();
            }
            else return BadRequest();
        }

        [Authorize]
        [HttpDelete("desfavoritar")]
        public async Task<ActionResult> DesfavoritarProduto([FromBody] ProdutoFavoritoDTO fav)
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
                var favoritos = await _favoritoService.GetAllAsync();
                var favorito = favoritos.FirstOrDefault(f => f.ProdutoId == fav.ProdutoId && f.UsuarioId == usuario.Id);

                if (favorito == null) return NotFound();

                var success = await _favoritoService.DeleteAsync(favorito.Id);
                if (!success) return NotFound();
                return NoContent();
            }
            else return BadRequest();
        }

        //[HttpGet("usuario/{usuarioId}")]
        //public async Task<ActionResult<IEnumerable<FavoritoDto>>> GetByUsuarioId(int usuarioId)
        //{
        //    var favoritos = await _favoritoService.GetByUsuarioIdAsync(usuarioId);
        //    if (!favoritos.Any()) return NotFound();
        //    return Ok(favoritos);
        //}
    }
}
