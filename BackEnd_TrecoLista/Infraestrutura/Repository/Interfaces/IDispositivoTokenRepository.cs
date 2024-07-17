using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IDispositivoTokenRepository
    {
        Task<DispositivoToken> GetByIdAsync(int id);
        Task<IEnumerable<DispositivoToken>> GetAllAsync();
        Task<DispositivoToken> AddAsync(DispositivoToken dispositivoToken);
        Task<DispositivoToken> UpdateAsync(DispositivoToken dispositivoToken);
        Task<bool> DeleteAsync(int id);
        Task<string> GetTokenByUserIdAsync(int userId);
        Task<List<string>> GetTokensByUserIdAsync(int userId);
    }
}
