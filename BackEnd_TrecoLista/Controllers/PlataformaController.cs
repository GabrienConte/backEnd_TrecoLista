using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEnd_TrecoLista.Application.ViewModel;

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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var plataformas = _plataformaRepository.Get();

            return Ok(plataformas);
        }
    }
}
