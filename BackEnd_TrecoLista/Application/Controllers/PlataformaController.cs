using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Application.ViewModel;
using BackEnd_TrecoLista.Domain.DTOs.Plataforma;
using BackEnd_TrecoLista.Domain.Services.Interfaces;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/plataforma")]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaService _plataformaService;

        public PlataformaController(IPlataformaService plataformaService)
        {
            _plataformaService = plataformaService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlataformaDto>>> GetPlataformas()
        {
            var plataformas = await _plataformaService.GetAllAsync();
            return Ok(plataformas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlataformaDto>> GetPlataforma(int id)
        {
            var plataforma = await _plataformaService.GetByIdAsync(id);
            if (plataforma == null)
            {
                return NotFound();
            }
            return Ok(plataforma);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PlataformaDto>> PostPlataforma(PlataformaCreateDto plataformaCreateDto)
        {
            var plataformaDto = await _plataformaService.AddAsync(plataformaCreateDto);
            return CreatedAtAction(nameof(GetPlataforma), new { id = plataformaDto.Id }, plataformaDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlataforma(int id, PlataformaUpdateDto plataformaUpdateDto)
        {
            await _plataformaService.UpdateAsync(id, plataformaUpdateDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlataforma(int id)
        {
            await _plataformaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
