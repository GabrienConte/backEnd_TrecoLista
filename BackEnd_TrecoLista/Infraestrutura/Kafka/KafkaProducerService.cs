using Confluent.Kafka;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Confluent.Kafka.Admin;
using System.Collections.Generic;

namespace BackEnd_TrecoLista.Infraestrutura.Kafka
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ProducerConfig _config;
        private readonly AdminClientConfig _adminConfig;
        private readonly string _bootstrapServers = "localhost:9092";

        public KafkaProducerService()
        {
            _config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            _adminConfig = new AdminClientConfig { BootstrapServers = _bootstrapServers };
        }

        public async Task EnviaNotificacaoAsync(int userId, string produtoDescricao, decimal novoPreco, decimal antigoPreco)
        {
            string topicName = $"notificacoes-user-{userId}";

            using (var adminClient = new AdminClientBuilder(_adminConfig).Build())
            {
                try
                {

                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    bool topicExists = metadata.Topics.Exists(t => t.Topic == topicName);

                    if (!topicExists)
                    {
                        await adminClient.CreateTopicsAsync(new TopicSpecification[]
                        {
                            new TopicSpecification { 
                                Name = topicName,
                                NumPartitions = 1, 
                                ReplicationFactor = 1, 
                                Configs = new Dictionary<string, string>
                                {
                                    { "retention.ms", "604800000" } // 1 semana em milissegundos
                                } 
                            }
                        });

                        Console.WriteLine($"Tópico {topicName} criado.");
                    }
                }
                catch (KafkaException ex)
                {
                    Console.WriteLine($"Erro ao verificar ou criar o tópico: {ex.Message}");
                    throw;
                }
            }

            KafkaMensagem kafkaMensagem = new KafkaMensagem
            {
                title = "Mudança de preço!",
                message = String.Format("{0} mudou de R${1} para R${2}", produtoDescricao, antigoPreco, novoPreco)
            };

            using (var producer = new ProducerBuilder<string, string>(_config).Build())
            {
                try
                {
                    string json = JsonSerializer.Serialize(kafkaMensagem);

                    var deliveryReport = await producer.ProduceAsync(
                        topicName, new Message<string, string>
                        {
                            Key = Guid.NewGuid().ToString(),
                            Value = json,
                        });

                    Console.WriteLine($"Mensagem enviada para o tópico: {deliveryReport.Topic}, Partição: {deliveryReport.Partition}, Offset: {deliveryReport.Offset}");
                }
                catch (ProduceException<string, string> e)
                {
                    Console.WriteLine($"Erro ao enviar a mensagem: {e.Error.Reason}");
                }
            }
        }
    }
}
