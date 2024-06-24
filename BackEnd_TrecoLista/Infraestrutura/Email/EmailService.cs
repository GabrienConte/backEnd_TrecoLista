using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using BackEnd_TrecoLista.Infraestrutura.Configurations;

namespace BackEnd_TrecoLista.Infraestrutura.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmailAsync(EmailRequisicao emailRequisicao)
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
    }
}
