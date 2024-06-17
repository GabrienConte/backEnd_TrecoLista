using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository;
using BackEnd_TrecoLista.Repository.Interfaces;
using BackEnd_TrecoLista.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadFoto(int id)
        {
            var produto = _produtoRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(produto.ImagemPath);

            return File(dataBytes, "image/png");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filePath = Path.Combine("Storage", produtoViewModel.Imagem.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            produtoViewModel.Imagem.CopyTo(fileStream);
            
            var produto = new Produto(
                produtoViewModel.Id,
                produtoViewModel.Descricao,
                produtoViewModel.Link,
                produtoViewModel.Valor,
                filePath,
                produtoViewModel.CategoriaId,
                produtoViewModel.PlataformaId
            );

            _produtoRepository.Add(produto);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var produtos = _produtoRepository.Get(pageNumber, pageQuantity);

            return Ok(produtos);
        }
    }
}
