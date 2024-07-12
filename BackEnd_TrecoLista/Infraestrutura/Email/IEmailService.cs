using AutoMapper.Internal;

namespace BackEnd_TrecoLista.Infraestrutura.Email
{
    public interface IEmailService
    {
        Task EnviarMudancaPrecoEmailAsync(int produtoId, int usuarioId, string toEmail, string produtoDescricao, decimal novoPreco, decimal antigoPreco, string linkProduto);
    }
}
