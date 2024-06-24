using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Domain.DTOs.Produto;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();
            return Ok(produtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetById(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> Create(ProdutoCreateDto produtoCreateDto)
        {
            var produto = await _produtoService.AddAsync(produtoCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDto>> Update(int id, ProdutoUpdateDto produtoUpdateDto)
        {
            var produto = await _produtoService.UpdateAsync(id, produtoUpdateDto);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _produtoService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }


        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public async Task<ActionResult> DownloadFoto(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);

            if (produto == null || string.IsNullOrEmpty(produto.ImagemPath))
            {
                return NotFound("Produto ou imagem não encontrada.");
            }

            try
            {
                var dataBytes = await System.IO.File.ReadAllBytesAsync(produto.ImagemPath);
                return File(dataBytes, "image/png", Path.GetFileName(produto.ImagemPath));
            }
            catch (FileNotFoundException)
            {
                return NotFound("Imagem não encontrada no servidor.");
            }
        }

        [HttpPost]
        [Route("/produtoScrap")]
        public async  Task<ActionResult> GetProdutoInfoScrap([FromBody] string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return BadRequest(new { error = "URL is required" });
            }

            try
            {
                var productInfo = await _produtoService.GetProductInfoAsync(request);
                return Ok(productInfo);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, new { error = e.Message });
            }

        }
    }
}
