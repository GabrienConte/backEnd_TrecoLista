using AutoMapper.Internal;
using BackEnd_TrecoLista.Infraestrutura.Email;
using MailKit;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] EmailRequisicao email)
        {
            try
            {
                await _emailService.SendEmailAsync(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
