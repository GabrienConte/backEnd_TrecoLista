using AutoMapper.Internal;

namespace BackEnd_TrecoLista.Infraestrutura.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequisicao email);
    }
}
