using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.FCM;
using BackEnd_TrecoLista.Infraestrutura.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TrecoLista.Application.Controllers
{
    [ApiController]
    [Route("api/v1/notificacao")]
    public class NotificationController : ControllerBase
    {
        private readonly IFCMService _fcmService;
        private readonly IKafkaProducerService _kafkaProducerService;
        private readonly IDispositivoTokenService _dispositivoTokenService;
        private readonly Dictionary<int, Task> _activeConsumers = new();

        public NotificationController(IFCMService fcmService, IDispositivoTokenService dispositivoTokenService, IKafkaProducerService kafkaProducerService)
        {
            _fcmService = fcmService;
            _kafkaProducerService = kafkaProducerService;
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

        [HttpPost("send-kafka")]
        public async Task<IActionResult> SendNotificationKafka(int userId, string produtoDescricao, decimal novoPreco, decimal antigoPreco)
        {
            await _kafkaProducerService.EnviaNotificacaoAsync(userId, produtoDescricao, novoPreco, antigoPreco);
            return Ok();
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
