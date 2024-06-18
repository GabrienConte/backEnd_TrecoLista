using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<IEnumerable<Plataforma>> GetAllAsync()
        {
            return await _context.Plataformas.ToListAsync();
        }

        public async Task<Plataforma> GetByIdAsync(int id)
        {
            return await _context.Plataformas.FindAsync(id);
        }

        public async Task<Plataforma> AddAsync(Plataforma plataforma)
        {
            var entityEntry = await _context.Plataformas.AddAsync(plataforma);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Plataforma> UpdateAsync(Plataforma plataforma)
        {
            var  plataformaAtualizada = _context.Plataformas.Update(plataforma);
            await _context.SaveChangesAsync();
            return plataformaAtualizada.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var plataforma = await _context.Plataformas.FindAsync(id);
            if (plataforma != null)
            {
                _context.Plataformas.Remove(plataforma);
                await _context.SaveChangesAsync();
            }
        }
    }

}
