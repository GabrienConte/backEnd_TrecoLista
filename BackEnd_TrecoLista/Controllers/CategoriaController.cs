using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Application.ViewModel;

namespace BackEnd_TrecoLista.Controllers
{
    [ApiController]
    [Route("api/v1/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CategoriaViewModel categoriaViewModel)
        {
            var categoria = new Categoria(
                0,
                categoriaViewModel.Descricao,
                categoriaViewModel.Ativo
                );
            _categoriaRepository.Add(categoria);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var categorias = _categoriaRepository.Get();

            return Ok(categorias);
        }
    }
}
