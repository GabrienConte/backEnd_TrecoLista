using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Domain.DTOs.Categoria;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("ativas")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategoriasAtivas()
        {
            var categorias = await _categoriaService.GetAtivasAsync();
            return Ok(categorias);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostCategoria(CategoriaCreateDto categoriaCreateDto)
        {
            var categoriaCriada = await _categoriaService.AddAsync(categoriaCreateDto);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaUpdateDto categoriaUpdateDto)
        {
            await _categoriaService.UpdateAsync(id, categoriaUpdateDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            await _categoriaService.DeleteAsync(id);
            return NoContent();
        }

    }
}
