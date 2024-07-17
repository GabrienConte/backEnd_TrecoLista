using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class DispositivoTokenRepository : IDispositivoTokenRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<DispositivoToken> GetByIdAsync(int id)
        {
            return await _context.DispositivoTokens.FindAsync(id);
        }

        public async Task<IEnumerable<DispositivoToken>> GetAllAsync()
        {
            return await _context.DispositivoTokens.ToListAsync();
        }

        public async Task<DispositivoToken> AddAsync(DispositivoToken dispositivoToken)
        {
            _context.DispositivoTokens.Add(dispositivoToken);
            await _context.SaveChangesAsync();
            return dispositivoToken;
        }

        public async Task<DispositivoToken> UpdateAsync(DispositivoToken dispositivoToken)
        {
            _context.DispositivoTokens.Update(dispositivoToken);
            await _context.SaveChangesAsync();
            return dispositivoToken;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dispositivoToken = await _context.DispositivoTokens.FindAsync(id);
            if (dispositivoToken == null)
            {
                return false;
            }

            _context.DispositivoTokens.Remove(dispositivoToken);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetTokenByUserIdAsync(int userId)
        {
            var dispositivoToken = await _context.DispositivoTokens
                .Where(dt => dt.UsuarioId == userId)
                .Select(dt => dt.Token)
                .FirstOrDefaultAsync();

            return dispositivoToken;
        }

        public async Task<List<string>> GetTokensByUserIdAsync(int userId)
        {
            var dispositivoTokens = await _context.DispositivoTokens
                .Where(dt => dt.UsuarioId == userId)
                .Select(dt => dt.Token)
                .ToListAsync();

            return dispositivoTokens;
        }
    }

}
