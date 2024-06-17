using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository;
using BackEnd_TrecoLista.Repository.Interfaces;
using BackEnd_TrecoLista.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Get()
        {
            var categorias = _categoriaRepository.Get();

            return Ok(categorias);
        }
    }
}
