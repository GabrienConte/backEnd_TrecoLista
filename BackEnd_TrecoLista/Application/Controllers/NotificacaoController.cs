using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.FCM;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/notificacao")]
    public class NotificationController : ControllerBase
    {
        private readonly IFCMService _fcmService;
        private readonly IDispositivoTokenService _dispositivoTokenService;

        public NotificationController(IFCMService fcmService, IDispositivoTokenService dispositivoTokenService)
        {
            _fcmService = fcmService;
            _dispositivoTokenService = dispositivoTokenService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            var token = await _dispositivoTokenService.GetTokenByUserIdAsync(request.UserId);

            if (token == null)
            {
                return BadRequest("Token not found");
            }

            var result = await _fcmService.EnviarNotificacaoAsync(token, request.Title, request.Message);
            return Ok(result);
        }

        [HttpPost("sendMultiple")]
        public async Task<IActionResult> SendNotificationToMultipleDevices([FromBody] MultiNotificationRequest request)
        {
            var tokens = await _dispositivoTokenService.GetTokensByUserIdsAsync(request.UserIds);

            if (tokens == null || tokens.Count == 0)
            {
                return BadRequest("Tokens not found");
            }

            var result = await _fcmService.EnviarNotificacaoParaVariosDispositivosAsync(tokens, request.Title, request.Message);
            return Ok(result);
        }
    }

    public class NotificationRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class MultiNotificationRequest
    {
        public List<int> UserIds { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
