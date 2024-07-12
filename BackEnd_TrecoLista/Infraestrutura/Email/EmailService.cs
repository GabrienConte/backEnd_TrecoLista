using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using BackEnd_TrecoLista.Infraestrutura.Configurations;
using System.Text;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Email
{
    public class EmailService : IEmailService
    {
        private readonly IHistoricoEmailRepository _repositoryHistoricoEmail;
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> emailConfig, IHistoricoEmailRepository historicoEmailRepository)
        {
            _repositoryHistoricoEmail = historicoEmailRepository;
            _emailConfig = emailConfig.Value;
        }

        private async Task EnviarEmailAsync(EmailRequisicao emailRequisicao)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.Sender = MailboxAddress.Parse(_emailConfig.Email);
                emailMessage.To.Add(MailboxAddress.Parse(emailRequisicao.Destinatario));
                emailMessage.Subject = emailRequisicao.Assunto;

                var builder = new BodyBuilder();
                builder.HtmlBody = emailRequisicao.Corpo;
                emailMessage.Body = builder.ToMessageBody();

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_emailConfig.Email, _emailConfig.Senha);
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Tratar a exceção conforme necessário (log, notificação, etc.)
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                throw; // Re-throw para propagar a exceção
            }
        }

        public async Task EnviarMudancaPrecoEmailAsync(int produtoId, int usuarioId, string destinatario, string produtoDescricao, decimal novoPreco, decimal antigoPreco, string linkProduto)
        {
            try
            {
                StringBuilder corpo = new StringBuilder();
                StringBuilder assunto = new StringBuilder();

                EmailRequisicao emailRequisicao = new EmailRequisicao();
                emailRequisicao.Destinatario = destinatario;
                emailRequisicao.Assunto = assunto.Append(String.Format("Mudou de preço o produto {0}", produtoDescricao)).ToString();
                emailRequisicao.Corpo = corpo.Append(
                    String.Format("O seu favorito {0} teve uma alteração de preço! Preço antigo:{1} Preço novo:{2}. Link de consulta: {3}",
                    produtoDescricao,
                    antigoPreco,
                    novoPreco,
                    linkProduto
                )).ToString();

                await EnviarEmailAsync(emailRequisicao);

                HistoricoEmail historicoEmail = new HistoricoEmail();
                historicoEmail.Assunto = emailRequisicao.Assunto;
                historicoEmail.CorpoEmail = emailRequisicao.Corpo;
                historicoEmail.DataEnvio = DateTime.UtcNow.ToUniversalTime(); ;
                historicoEmail.DestinoId = usuarioId;
                historicoEmail.ProdutoId = produtoId;

                await _repositoryHistoricoEmail.AddAsync(historicoEmail);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
