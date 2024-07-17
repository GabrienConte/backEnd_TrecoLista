using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd_TrecoLista.Infraestrutura.WebSocketHost
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public WebSocketMiddleware(RequestDelegate next, IHostApplicationLifetime appLifetime)
        {
            _next = next;
            _appLifetime = appLifetime;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                if (context.Request.Query.TryGetValue("userId", out var userIdValue) && int.TryParse(userIdValue, out var userId))
                {
                    using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        Console.WriteLine($"WebSocket connection established for user: {userId}");
                        await HandleWebSocketAsync(webSocket, userId, _cancellationTokenSource.Token);
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("User ID is required.");
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleWebSocketAsync(WebSocket webSocket, int userId, CancellationToken cancellationToken)
        {
            string topicName = $"notificacoes-user-{userId}";
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = $"notificacoes-group-user-{userId}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topicName);
                Console.WriteLine($"Topic to consume: {topicName}");

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(0);
                        if (consumeResult != null)
                        {
                            var message = Encoding.UTF8.GetBytes(consumeResult.Message.Value);
                            Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                            await webSocket.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, cancellationToken);
                        }  else
                        {
                            Thread.Sleep(5000);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Consumo foi cancelado
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
                    }
                }

                consumer.Close();
            }
        }
    }
}
