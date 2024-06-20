using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Domain.Model;
using Microsoft.EntityFrameworkCore;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Set<Produto>().Include(p => p.Categoria).Include(p => p.Plataforma).ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Set<Produto>().Include(p => p.Categoria).Include(p => p.Plataforma).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            var entity = await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            var entity = _context.Set<Produto>().Update(produto);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produto = await _context.Set<Produto>().FindAsync(id);
            if (produto == null) return false;

            _context.Set<Produto>().Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
