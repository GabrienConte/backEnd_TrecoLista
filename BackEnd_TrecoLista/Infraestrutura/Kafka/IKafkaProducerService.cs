using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Kafka
{
    public interface IKafkaProducerService
    {
        Task EnviaNotificacaoAsync(int userId, string produtoDescricao, decimal novoPreco, decimal antigoPreco);
    }
}
