namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IFCMService
    {
        Task<string> EnviarNotificacaoAsync(string token, string title, string body);
        Task<string> EnviarNotificacaoParaVariosDispositivosAsync(List<string> tokens, string title, string body);
    }
}