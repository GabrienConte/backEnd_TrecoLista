using BackEnd_TrecoLista.Domain.DTOs.DispositivoToken;

namespace BackEnd_TrecoLista.Domain.Services.Interfaces
{
    public interface IDispositivoTokenService
    {
        Task<DispositivoTokenDto> GetByIdAsync(int id);
        Task<IEnumerable<DispositivoTokenDto>> GetAllAsync();
        Task AddOrUpdateDeviceTokenAsync(DispositivoTokenCreateDto dispositivoTokenCreateDto);
        Task<bool> DeleteAsync(int id);
        Task<string> GetTokenByUserIdAsync(int userId);
        Task<List<string>> GetTokensByUserIdsAsync(List<int> userIds);
        Task EnviarNotificacaoMudouPrecoToUserAsync(int userId, string produtoDescricao, decimal novoPreco, decimal antigoPreco);
        Task EnviarNotificacaoMudouPrecoToMultipleUsersAsync(List<int> userIds, string produtoDescricao, decimal novoPreco, decimal antigoPreco);
    }
}
