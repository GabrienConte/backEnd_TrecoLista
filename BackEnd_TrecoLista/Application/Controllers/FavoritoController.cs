using BackEnd_TrecoLista.Domain.DTOs.Favorito;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/favorito")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;

        public FavoritoController(IFavoritoService favoritoService)
        {
            _favoritoService = favoritoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoritoDto>>> GetAll()
        {
            var favoritos = await _favoritoService.GetAllAsync();
            return Ok(favoritos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoritoDto>> GetById(int id)
        {
            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null) return NotFound();
            return Ok(favorito);
        }

        [HttpPost]
        public async Task<ActionResult<FavoritoDto>> Create(FavoritoCreateDto favoritoCreateDto)
        {
            var favorito = await _favoritoService.AddAsync(favoritoCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = favorito.Id }, favorito);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FavoritoDto>> Update(int id, FavoritoUpdateDto favoritoUpdateDto)
        {
            var favorito = await _favoritoService.UpdateAsync(id, favoritoUpdateDto);
            if (favorito == null) return NotFound();
            return Ok(favorito);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _favoritoService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("favoritar")]
        public async Task<ActionResult<FavoritoDto>> FavoritarProduto([FromBody] FavoritoCreateDto favoritoCreateDto)
        {
            var favorito = await _favoritoService.AddAsync(favoritoCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = favorito.Id }, favorito);
        }

        [HttpDelete("desfavoritar")]
        public async Task<ActionResult> DesfavoritarProduto([FromBody] FavoritoCreateDto favoritoCreateDto)
        {
            var favoritos = await _favoritoService.GetAllAsync();
            var favorito = favoritos.FirstOrDefault(f => f.ProdutoId == favoritoCreateDto.ProdutoId && f.UsuarioId == favoritoCreateDto.UsuarioId);

            if (favorito == null) return NotFound();

            var success = await _favoritoService.DeleteAsync(favorito.Id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<FavoritoDto>>> GetByUsuarioId(int usuarioId)
        {
            var favoritos = await _favoritoService.GetByUsuarioIdAsync(usuarioId);
            if (!favoritos.Any()) return NotFound();
            return Ok(favoritos);
        }
    }
}
