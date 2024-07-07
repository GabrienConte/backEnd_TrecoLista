using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Domain.DTOs.Produto;
using System.Security.Claims;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IFavoritoService _favoritoService;

        public ProdutoController(IProdutoService produtoService, IFavoritoService favoritoService)
        {
            _produtoService = produtoService;
            _favoritoService = favoritoService; 
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
        //{
        //    var produtos = await _produtoService.GetAllAsync();
        //    return Ok(produtos);
        //}

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetById(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [Authorize]
        [HttpGet("{produtoId}/favoritado")]
        public async Task<ActionResult<ProdutoFavoritadoDTO>> GetProdutoDetalhadoByUserAndProduto(int produtoId)
        {
            var userId = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var produto = await _produtoService.GetProdutoFavoritadoByUserAndProdutoAsync(int.Parse(userId), produtoId);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> Create([FromForm] ProdutoCreateDto produtoCreateDto)
        {
            var userId = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var produto = await _produtoService.AddAsync(produtoCreateDto, int.Parse(userId));
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

        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var success = await _produtoService.DeleteAsync(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}


        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public async Task<ActionResult> DownloadFoto(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);

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
        [Route("produtoScrap")]
        public async Task<ActionResult> GetProdutoInfoScrap([FromBody] GetProdutoScrapRequest request)
        {
            if (string.IsNullOrEmpty(request.link))
            {
                return BadRequest(new { error = "URL is required" });
            }

            try
            {
                var productInfo = await _produtoService.GetProductInfoAsync(request.link);
                return Ok(productInfo);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, new { error = e.Message });
            }

        }

        [Authorize]
        [HttpGet("Favoritados")]
        public async Task<ActionResult<IEnumerable<ProdutoCardDTO>>> GetProdutosFavoritadosCards()
        {
            var userId = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var favoritos = await _favoritoService.GetByUsuarioIdAsync(int.Parse(userId));
            if (!favoritos.Any()) return NoContent();

            var produtosDetalhes = new List<ProdutoCardDTO>();

            foreach (var favorito in favoritos)
            {
                var produto = await _produtoService.GetProdutoByIdAsync(favorito.ProdutoId);
                if (produto != null)
                {
                    produtosDetalhes.Add(new ProdutoCardDTO
                    {
                        ProdutoId = produto.Id,
                        Link = produto.Link,
                        Descricao = produto.Descricao,
                        Valor = produto.Valor,
                        ImagemPath = produto.ImagemPath,
                        IsFavoritado = true
                    });
                }
            }

            return Ok(produtosDetalhes);
        }

        [Authorize]
        [HttpGet("NaoFavoritados")]
        public async Task<ActionResult<IEnumerable<ProdutoCardDTO>>> GetProdutosNaoFavoritadosCards()
        {
            var userId = User.FindFirst("usuario_id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var favoritos = await _favoritoService.GetByUsuarioIdAsync(int.Parse(userId));
            var favoritosProdutoIds = favoritos.Select(f => f.ProdutoId).ToList();

            var todosProdutos = await _produtoService.GetAllAsync();
            var produtosNaoFavoritados = todosProdutos
                .Where(p => !favoritosProdutoIds.Contains(p.Id))
                .Select(p => new ProdutoCardDTO
                {
                    ProdutoId = p.Id,
                    Link = p.Link,
                    Descricao = p.Descricao,
                    Valor = p.Valor,
                    ImagemPath = p.ImagemPath,
                    IsFavoritado = false
                })
                .ToList();

            return Ok(produtosNaoFavoritados);
        }
    }
}
