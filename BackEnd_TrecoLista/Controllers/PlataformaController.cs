using BackEnd_TrecoLista.Model;
using BackEnd_TrecoLista.Repository;
using BackEnd_TrecoLista.Repository.Interfaces;
using BackEnd_TrecoLista.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Controllers
{
    [ApiController]
    [Route("api/v1/plataforma")]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public PlataformaController(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add(PlataformaViewModel plataformaViewModel)
        {
            var plataforma = new Plataforma(
                0,
                plataformaViewModel.Descricao
                );
            _plataformaRepository.Add(plataforma);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var plataformas = _plataformaRepository.Get();

            return Ok(plataformas);
        }
    }
}
