using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<Favorito>> GetAllAsync()
        {
            return await _context.Set<Favorito>()
                .Include(f => f.Produto)
                .Include(f => f.Usuario)
                .ToListAsync();
        }

        public async Task<Favorito> GetByIdAsync(int id)
        {
            return await _context.Set<Favorito>()
                .Include(f => f.Produto)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Favorito> GetByUserIdAndProdutoIdAsync(int userId, int produtoId)
        {
            return await _context.Favoritos.FirstOrDefaultAsync(f => f.ProdutoId == produtoId && f.UsuarioId == userId);
        }

        public async Task<Favorito> AddAsync(Favorito favorito)
        {
            var entity = await _context.Set<Favorito>().AddAsync(favorito);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Favorito> UpdateAsync(Favorito favorito)
        {
            var entity = _context.Set<Favorito>().Update(favorito);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var favorito = await _context.Set<Favorito>().FindAsync(id);
            if (favorito == null) return false;

            _context.Set<Favorito>().Remove(favorito);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Favorito>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Set<Favorito>().Where(f => f.UsuarioId == usuarioId).ToListAsync();
        }
    }
}
