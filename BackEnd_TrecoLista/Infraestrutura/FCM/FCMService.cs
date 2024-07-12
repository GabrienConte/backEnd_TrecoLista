using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using BackEnd_TrecoLista.Domain.Services.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.FCM
{
    public class FCMService : IFCMService
    {
        private static FirebaseApp _firebaseApp;
        public FCMService()
        {
            if (_firebaseApp == null)
            {
                _firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("Infraestrutura/Configurations/treco-lista-firebase-admin.json")
                });
            }
        }

        public async Task<string> EnviarNotificacaoAsync(string token, string titulo, string mensagem)
        {
            var message = new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Title = titulo,
                    Body = mensagem
                }
            };

            // Envia a mensagem
            return await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public async Task<string> EnviarNotificacaoParaVariosDispositivosAsync(List<string> tokens, string titulo, string mensagem)
        {
            var message = new MulticastMessage()
            {
                Tokens = tokens,
                Notification = new Notification()
                {
                    Title = titulo,
                    Body = mensagem
                }
            };

            // Envia a mensagem
            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            return $"Successfully sent {response.SuccessCount} messages";
        }
    }
}
