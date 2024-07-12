using BackEnd_TrecoLista.Infraestrutura;
using BackEnd_TrecoLista.Domain.Model;
using Microsoft.EntityFrameworkCore;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;

namespace BackEnd_TrecoLista.Infraestrutura.Repository
{
    public class HistoricoEmailRepository : IHistoricoEmailRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task AddAsync(HistoricoEmail historicoEmail)
        {
            var entity = await _context.HistoricoEmails.AddAsync(historicoEmail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HistoricoEmail>> GetAllAsync()
        {
            return _context.HistoricoEmails
                .Include(he => he.Destino)
                .Include(he => he.Produto)
                .ToList();
        }
    }
}
